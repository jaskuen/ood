using Figures.Model.Core.Events;

namespace Figures.ViewModel;

/// <summary>
/// Управляет множеством окон редактора.
/// </summary>
public class WindowManager(DocumentEditor? defaultSession = null) : EventEmitter
{
    private readonly List<WindowViewModel> _windows = new();
    private string? _activeId;

    private DocumentEditor? DefaultSession { get; } = defaultSession;

    public WindowViewModel CreateWindow()
    {
        var session = DefaultSession ?? new DocumentEditor();
        var window = new WindowViewModel(Guid.NewGuid().ToString(), session);
        _windows.Add(window);
        if (_activeId == null)
        {
            SetActive(window.Id);
        }
        Emit(EventType.WindowCreated, window);
        return window;
    }

    public void CloseWindow(string id)
    {
        var index = _windows.FindIndex(w => w.Id == id);
        if (index == -1) return;
        var removed = _windows[index];
        _windows.RemoveAt(index);
        Emit(EventType.WindowClosed, removed);
        if (_activeId == id)
        {
            _activeId = _windows.FirstOrDefault()?.Id;
            if (_activeId != null)
            {
                Emit(EventType.ActiveWindowChanged, GetActive());
            }
        }
    }

    public void SetActive(string id)
    {
        if (_activeId == id) return;
        _activeId = id;
        Emit(EventType.ActiveWindowChanged, GetActive());
    }

    public WindowViewModel? GetActive()
    {
        return _windows.FirstOrDefault(w => w.Id == _activeId);
    }

    public List<WindowViewModel> GetAll()
    {
        return new List<WindowViewModel>(_windows);
    }
}

