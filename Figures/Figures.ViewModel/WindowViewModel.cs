using Figures.Model.Core.Events;
using Figures.Model.Selection;

namespace Figures.ViewModel;

/// <summary>
/// ViewModel для отдельного окна редактора.
/// </summary>
public class WindowViewModel(string id, DocumentEditor session) : EventEmitter
{
    public Selection Selection { get; } = new();
    public string Id { get; } = id;
    public DocumentEditor Session { get; } = session;

    public void SelectFigure(string? id)
    {
        Selection.Select(id);
        Emit(EventType.SelectionChanged, id);
    }

    public void ClearSelection()
    {
        Selection.Clear();
        Emit(EventType.SelectionChanged, null);
    }
}

