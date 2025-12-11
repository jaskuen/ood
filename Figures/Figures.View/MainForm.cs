using System.Text;
using DrawingSize = System.Drawing.Size;
using Figures.Model;
using Figures.Model.Core;
using Figures.Model.Core.Events;
using Figures.ViewModel;

namespace Figures.View;

public sealed class MainForm : Form
{
    private readonly WindowManager _windowManager;
    private WindowViewModel _activeWindow;
    private readonly CanvasControl _canvas;
    private readonly Label _propertiesLabel;
    private string _lastPropertiesText = string.Empty;

    public MainForm()
    {
        Text = "Figures";
        MinimumSize = new DrawingSize(1100, 750);

        _windowManager = new WindowManager();
        _activeWindow = _windowManager.CreateWindow();

        var toolbar = BuildToolbar();

        _canvas = new CanvasControl();

        var canvasHost = new Panel { Dock = DockStyle.Fill, BackColor = Color.LightGray };
        canvasHost.Controls.Add(_canvas);

        _propertiesLabel = new Label
        {
            Dock = DockStyle.Fill,
            AutoSize = false,
            Padding = new Padding(8),
            Font = new Font("Segoe UI", 10),
            BackColor = Color.WhiteSmoke
        };
        
        // Двойная буферизация
        typeof(Label).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
            null, _propertiesLabel, new object[] { true });

        var propertiesPanel = new Panel
        {
            Dock = DockStyle.Right,
            Width = 260,
            BackColor = Color.WhiteSmoke
        };
        
        // Двойная буферизация
        typeof(Panel).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
            null, propertiesPanel, new object[] { true });
        propertiesPanel.Controls.Add(_propertiesLabel);

        Controls.Add(canvasHost);
        Controls.Add(propertiesPanel);
        Controls.Add(toolbar);

        BindWindow(_activeWindow);
    }

    private Control BuildToolbar()
    {
        var toolbar = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 52,
            AutoSize = false,
            FlowDirection = FlowDirection.LeftToRight,
            Padding = new Padding(8),
            BackColor = Color.Gainsboro
        };

        toolbar.Controls.Add(CreateButton("Прямоугольник", (_, _) => AddFigure(FigureKind.Rectangle)));
        toolbar.Controls.Add(CreateButton("Эллипс", (_, _) => AddFigure(FigureKind.Ellipse)));
        toolbar.Controls.Add(CreateButton("Треугольник", (_, _) => AddFigure(FigureKind.Triangle)));
        toolbar.Controls.Add(CreateButton("Изображение", async void (_, _) => await AddImageAsync()));
        toolbar.Controls.Add(CreateButton("Удалить", (_, _) => DeleteSelection()));
        toolbar.Controls.Add(CreateButton("Отменить", (_, _) => _activeWindow.Session.Undo()));
        toolbar.Controls.Add(CreateButton("Повторить", (_, _) => _activeWindow.Session.Redo()));
        toolbar.Controls.Add(CreateButton("Сохранить", async (_, _) => await SaveDocumentAsync()));
        toolbar.Controls.Add(CreateButton("Сохранить как", async (_, _) => await SaveDocumentAsAsync()));
        toolbar.Controls.Add(CreateButton("Открыть", async (_, _) => await OpenDocumentAsync()));
        toolbar.Controls.Add(CreateButton("Новый", (_, _) => NewDocument()));

        return toolbar;
    }

    private Button CreateButton(string text, EventHandler onClick)
    {
        return new Button
        {
            Text = text,
            AutoSize = true,
            Margin = new Padding(4),
            Padding = new Padding(8, 4, 8, 4),
            UseVisualStyleBackColor = true
        }.Also(b => b.Click += onClick);
    }

    private void BindWindow(WindowViewModel window)
    {
        AttachWindowEvents(window);
        _canvas.Bind(window);
        RefreshProperties();
        UpdateTitle();
    }

    private void AttachWindowEvents(WindowViewModel window)
    {
        window.Selection.On(EventType.SelectionChanged, _ => RefreshProperties());
        var doc = window.Session.Document;
        doc.On(EventType.FigureAdded, _ => RefreshProperties());
        doc.On(EventType.FigureRemoved, _ => RefreshProperties());
        doc.On(EventType.FigureChanged, payload =>
        {
            // Обновляем параметры в тот момент, когда закончили перемещение
            if (payload is not null)
            {
                try
                {
                    dynamic changeInfo = payload;
                    if (changeInfo.Reason?.ToString() == "geometry")
                    {
                        return;
                    }
                }
                catch
                {
                    // Если не удалось получить данные, обновляем параметры
                }
            }
            RefreshProperties();
        });
        doc.On(EventType.CanvasChanged, _ => RefreshProperties());
        doc.On(EventType.Cleared, _ => RefreshProperties());
        window.Session.On(EventType.Saved, _ => UpdateTitle());
        window.Session.On(EventType.Loaded, _ =>
        {
            RefreshProperties();
            UpdateTitle();
        });
        window.Session.Document.On(EventType.NameChanged, _ => UpdateTitle());
        // Refresh properties when commands are applied (e.g., when drag completes)
        window.Session.On(EventType.CommandApplied, _ => RefreshProperties());
    }

    private void UpdateTitle()
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(UpdateTitle));
            return;
        }

        var fileName = _activeWindow.Session.CurrentFilePath != null
            ? Path.GetFileName(_activeWindow.Session.CurrentFilePath)
            : _activeWindow.Session.Document.Name;
        Text = $"Figures - {fileName}";
    }

    private void RefreshProperties()
    {
        if (InvokeRequired)
        {
            BeginInvoke(RefreshProperties);
            return;
        }

        var sb = new StringBuilder();
        var doc = _activeWindow.Session.Document;
        var selectedId = _activeWindow.Selection.SelectedId;

        if (selectedId == null)
        {
            sb.AppendLine("Свойства холста");
            sb.AppendLine($"Ширина: {doc.CanvasWidth:F0}");
            sb.AppendLine($"Высота: {doc.CanvasHeight:F0}");
        }
        else
        {
            var figure = doc.GetFigure(selectedId);
            if (figure != null)
            {
                var box = figure.GetBoundingBox();
                var attrs = figure.Attributes;
                sb.AppendLine("Свойства фигуры");
                sb.AppendLine($"ID: {figure.Id}");
                sb.AppendLine($"X: {box.X:F1}");
                sb.AppendLine($"Y: {box.Y:F1}");
                sb.AppendLine($"Ширина: {box.Width:F1}");
                sb.AppendLine($"Высота: {box.Height:F1}");
                sb.AppendLine($"Заливка: #{attrs.Fill:X8}");
                sb.AppendLine($"Обводка: #{attrs.Stroke:X8}");
                sb.AppendLine($"Толщина: {attrs.StrokeWidth:F1}");
            }
        }

        var newText = sb.ToString();
        // Only update if the text actually changed to prevent unnecessary repaints
        if (newText != _lastPropertiesText)
        {
            _propertiesLabel.SuspendLayout();
            _propertiesLabel.Text = newText;
            _lastPropertiesText = newText;
            _propertiesLabel.ResumeLayout();
        }
    }

    private void AddFigure(FigureKind kind)
    {
        var id = _activeWindow.Session.AddFigure(kind);
        _activeWindow.SelectFigure(id);
    }

    private async Task AddImageAsync()
    {
        using var dialog = new OpenFileDialog();
        dialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.gif;*.webp";
        dialog.Multiselect = false;

        if (dialog.ShowDialog() != DialogResult.OK) return;

        try
        {
            var data = await File.ReadAllBytesAsync(dialog.FileName);
            var id = await _activeWindow.Session.AddImageFromBytesAsync(data, Path.GetFileName(dialog.FileName));
            if (id != null)
            {
                _activeWindow.SelectFigure(id);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Не удалось загрузить изображение: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DeleteSelection()
    {
        var selectedId = _activeWindow.Selection.SelectedId;
        if (selectedId == null) return;
        _activeWindow.Session.DeleteFigure(selectedId);
        _activeWindow.SelectFigure(null);
    }

    private async Task SaveDocumentAsync()
    {
        var saved = await _activeWindow.Session.SaveAsync();
        if (!saved)
        {
            // No current path, use Save As instead
            await SaveDocumentAsAsync();
            return;
        }
        MessageBox.Show(this, "Документ сохранён", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async Task SaveDocumentAsAsync()
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "JSON документ|*.json",
            FileName = $"{_activeWindow.Session.Document.Name}.json"
        };

        if (dialog.ShowDialog() != DialogResult.OK) return;
        await _activeWindow.Session.SaveAsAsync(dialog.FileName);
        MessageBox.Show(this, "Документ сохранён", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async Task OpenDocumentAsync()
    {
        using var dialog = new OpenFileDialog();
        dialog.Filter = "JSON документ|*.json";
        dialog.Multiselect = false;

        if (dialog.ShowDialog() != DialogResult.OK) return;

        await _activeWindow.Session.OpenAsync(dialog.FileName);
        _activeWindow.ClearSelection();
    }

    private void NewDocument()
    {
        _activeWindow = _windowManager.CreateWindow();
        BindWindow(_activeWindow);
    }
}

internal static class ControlExtensions
{
    public static T Also<T>(this T instance, Action<T> action)
    {
        action(instance);
        return instance;
    }
}

