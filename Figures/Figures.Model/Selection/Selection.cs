using Figures.Model.Core.Events;

namespace Figures.Model.Selection;

/// <summary>
/// Управляет текущим выделением фигур на холсте.
/// </summary>
public class Selection : EventEmitter
{
    private string? _selectedId;

    public string? SelectedId => _selectedId;

    public void Select(string? id)
    {
        if (id == _selectedId) return;
        _selectedId = id;
        Emit(EventType.SelectionChanged, id);
    }

    public void Clear()
    {
        Select(null);
    }

    public bool IsSelected(string id)
    {
        return _selectedId == id;
    }
}

