using Figures.Model.Core;
using Figures.Model.Core.Events;
using Figures.Model.Figures;

namespace Figures.Model.Document;

/// <summary>
/// Модель документа - единственный источник истины для данных документа.
/// </summary>
public class DocumentModel(string name = "Без названия", float width = 960, float height = 640)
    : EventEmitter
{
    private readonly List<FigureModel> _figures = [];
    private float _canvasWidth = width;
    private float _canvasHeight = height;
    private string _name = name;

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            Emit(EventType.NameChanged, value);
        }
    }

    public float CanvasWidth => _canvasWidth;
    public float CanvasHeight => _canvasHeight;
    public List<FigureModel> Figures => new(_figures);

    public void SetCanvasSize(float width, float height)
    {
        if (width == _canvasWidth && height == _canvasHeight) return;
        _canvasWidth = Math.Max(100, width);
        _canvasHeight = Math.Max(100, height);
        Emit(EventType.CanvasChanged, new { Width = _canvasWidth, Height = _canvasHeight });
    }

    public FigureModel? GetFigure(string id)
    {
        return _figures.FirstOrDefault(f => f.Id == id);
    }

    public void AddFigure(FigureModel figure, int? index = null)
    {
        if (index.HasValue && index >= 0 && index <= _figures.Count)
        {
            _figures.Insert(index.Value, figure);
        }
        else
        {
            _figures.Add(figure);
        }
        Emit(EventType.FigureAdded, figure);
    }

    public FigureModel? RemoveFigure(string id)
    {
        var index = _figures.FindIndex(f => f.Id == id);
        if (index == -1) return null;
        var removed = _figures[index];
        _figures.RemoveAt(index);
        Emit(EventType.FigureRemoved, removed);
        return removed;
    }

    public void UpdateFigure(string id, Action<FigureModel> updater, string reason)
    {
        var figure = GetFigure(id);
        if (figure == null) return;
        updater(figure);
        Emit(EventType.FigureChanged, new { Figure = figure, Reason = reason });
    }

    public void Clear()
    {
        _figures.Clear();
        Emit(EventType.Cleared, null);
    }

    public DocumentData Serialize()
    {
        return new DocumentData
        {
            Version = "1.0.0",
            FileName = _name,
            Canvas = new CanvasData { Width = _canvasWidth, Height = _canvasHeight },
            Figures = _figures.Select(f => f.Serialize()).ToList(),
            Images = new Dictionary<string, ImageData>()
        };
    }
}

