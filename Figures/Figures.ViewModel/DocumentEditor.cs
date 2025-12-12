using Figures.Model.Core;
using Figures.Model.Core.Events;
using Figures.Model.Core.Utils;
using Figures.Model.Document;
using Figures.Model.Figures;
using Figures.ViewModel.Commands;
using Figures.ViewModel.Services;
using Figures.ViewModel.Services.Storage;

namespace Figures.ViewModel;

/// <summary>
/// Редактор документа
/// </summary>
public class DocumentEditor : EventEmitter
{
    public DocumentModel Document { get; }
    public CommandHistory History { get; }
    private ImageStorage _imageStorage;
    private readonly IDocumentStorage _documentStorage;
    public string? CurrentFilePath { get; private set; }

    public DocumentEditor(
        DocumentModel? document = null,
        IDocumentStorage? documentStorage = null,
        ImageStorage? imageStorage = null)
    {
        Document = document ?? new DocumentModel();
        History = new CommandHistory();
        _imageStorage = imageStorage ?? new ImageStorage();
        _documentStorage = documentStorage ?? new JsonDocumentStorage();
        BindImageLifecycle();
    }

    public string AddFigure(FigureKind kind)
    {
        var box = DefaultBox();
        FigureModel figure = kind switch
        {
            FigureKind.Rectangle => FigureFactory.CreateRectangle(box),
            FigureKind.Ellipse => FigureFactory.CreateEllipse(box),
            FigureKind.Triangle => FigureFactory.CreateTriangle(box),
            _ => throw new ArgumentException($"Unsupported figure type: {kind}")
        };

        var command = new AddFigureCommand(Document, figure);
        History.Execute(command);
        EmitCommandEvent("execute", command);
        return command.FigureId;
    }

    public async Task<string?> AddImageFromBytesAsync(byte[] data, string? name = null)
    {
        var (id, stored) = await _imageStorage.StoreFileAsync(data, name);
        var figure = FigureFactory.CreateImage(DefaultBox(), id, stored);
        var command = new AddFigureCommand(Document, figure);
        History.Execute(command);
        EmitCommandEvent("execute", command);
        return command.FigureId;
    }

    public void DeleteFigure(string figureId)
    {
        var figure = Document.GetFigure(figureId);
        if (figure == null) return;
        var command = new DeleteFigureCommand(Document, figure);
        History.Execute(command);
        EmitCommandEvent("execute", command);
    }

    public void PreviewTransform(string figureId, Rect box)
    {
        var limited = Geometry.LimitRectToCanvas(box, new Size(Document.CanvasWidth, Document.CanvasHeight));
        Document.UpdateFigure(figureId, figure => figure.Resize(limited), "geometry");
    }

    public void CommitTransform(string figureId, Rect from, Rect to)
    {
        float tolerance = 0.001f;

        var limited = Geometry.LimitRectToCanvas(to, new Size(Document.CanvasWidth, Document.CanvasHeight));
        var changes = Math.Abs(from.X - limited.X) > tolerance ||
                      Math.Abs(from.Y - limited.Y) > tolerance ||
                      Math.Abs(from.Width - limited.Width) > tolerance ||
                      Math.Abs(from.Height - limited.Height) > tolerance;
        if (!changes)
        {
            return;
        }

        var command = new TransformFigureCommand(Document, figureId, from, limited);
        History.Execute(command);
        EmitCommandEvent("execute", command);
    }

    public void UpdateAttributes(string figureId, FigureAttributes attributes)
    {
        var figure = Document.GetFigure(figureId);
        if (figure == null) return;
        var from = figure.Attributes;
        var command = new UpdateAttributesCommand(Document, figureId, from, attributes);
        History.Execute(command);
        EmitCommandEvent("execute", command);
    }

    public void Undo()
    {
        var command = History.Undo();
        EmitCommandEvent("undo", command);
    }

    public void Redo()
    {
        var command = History.Redo();
        EmitCommandEvent("redo", command);
    }

    /// <summary>
    /// Сохранить
    /// </summary>
    public async Task<bool> SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(CurrentFilePath))
        {
            return false; // Нет пути
        }

        await SaveAsAsync(CurrentFilePath);
        return true;
    }

    /// <summary>
    /// Сохранить как
    /// </summary>
    public async Task SaveAsAsync(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path is required to save document", nameof(path));
        }

        var data = Document.Serialize();
        var imageIds = data.Figures
            .Where(f => f.Type == FigureKind.Image)
            .Select(f => f.Data["imageId"].ToString() ?? "")
            .ToArray();
        data.Images = _imageStorage.Export(imageIds);
        await _documentStorage.SaveAsync(path, data);
        CurrentFilePath = path;
        Document.Name = Path.GetFileNameWithoutExtension(path);
        Emit(EventType.Saved, path);
    }

    public async Task OpenAsync(string path)
    {
        var data = await _documentStorage.OpenAsync(path);
        if (data != null)
        {
            Load(data);
            CurrentFilePath = path;
            Document.Name = Path.GetFileNameWithoutExtension(path) ?? Document.Name;
        }
    }

    public void SetCanvasSize(float width, float height)
    {
        Document.SetCanvasSize(width, height);
    }

    public string GetDocumentFileExtension() => _documentStorage.GetFileExtension();
    public string GetDocumentFileType() => _documentStorage.GetFileType();

    private void Load(DocumentData data)
    {
        History.Clear();
        Document.Clear();
        Document.SetCanvasSize(data.Canvas.Width, data.Canvas.Height);
        Document.Name = data.FileName ?? "Без названия";

        _imageStorage.Dispose();
        _imageStorage = new ImageStorage();

        // Все картинки из хранилища
        var images = data.Images ?? new Dictionary<string, ImageData>();
        foreach (var (id, info) in images)
        {
            var base64 = info.DataUrl.Split(',')[1];
            var imageData = Convert.FromBase64String(base64);
            _imageStorage.StoreDataAsync(id, imageData, info.Name).Wait();
        }

        // Создаем фигуры
        foreach (var serialized in data.Figures)
        {
            var figure = FigureFactory.FromSerialized(serialized, imageId =>
            {
                var data = _imageStorage.GetImageData(imageId);
                return data;
            });

            // Для картинок проверяем, что данные корректны
            if (figure is ImageModel imageModel)
            {
                var imageData = _imageStorage.GetImageData(imageModel.ImageId);
                if (imageData != null)
                {
                    imageModel.SetImageSource(imageModel.ImageId, imageData);
                }
            }

            Document.AddFigure(figure);
        }

        Emit(EventType.Loaded, null);
    }

    private void BindImageLifecycle()
    {
        Document.On(EventType.FigureAdded, payload =>
        {
            if (payload is ImageModel image)
            {
                _imageStorage.Retain(image.ImageId);
                var data = _imageStorage.GetImageData(image.ImageId);
                if (data != null)
                {
                    image.SetImageSource(image.ImageId, data);
                }
            }
        });

        Document.On(EventType.FigureRemoved, payload =>
        {
            if (payload is ImageModel image)
            {
                _imageStorage.Release(image.ImageId);
            }
        });
    }

    private Rect DefaultBox()
    {
        var width = Math.Min(200, Document.CanvasWidth * 0.25f);
        var height = Math.Min(150, Document.CanvasHeight * 0.25f);
        return new Rect(
            (Document.CanvasWidth - width) / 2,
            (Document.CanvasHeight - height) / 2,
            width,
            height
        );
    }

    private void EmitCommandEvent(string action, ICommand? command)
    {
        if (command == null) return;
        var figureId = command switch
        {
            AddFigureCommand cmd => cmd.FigureId,
            DeleteFigureCommand cmd => cmd.FigureId,
            TransformFigureCommand cmd => cmd.FigureId,
            UpdateAttributesCommand cmd => cmd.FigureId,
            _ => null
        };
        Emit(EventType.CommandApplied, new { Action = action, FigureId = figureId });
    }
}