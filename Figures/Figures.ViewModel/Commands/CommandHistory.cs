using Figures.Model.Core.Events;

namespace Figures.ViewModel.Commands;

/// <summary>
/// Управляет историей команд, позволяя выполнять операции отмены (undo) и повтора (redo).
/// </summary>
public class CommandHistory(int limit = 20) : EventEmitter
{
    private readonly List<ICommand> _stack = [];
    private int _index = -1;

    /// <summary>
    /// Выполняет новую команду и добавляет ее в историю.
    /// </summary>
    public void Execute(ICommand command)
    {
        if (_index < _stack.Count - 1)
        {
            _stack.RemoveRange(_index + 1, _stack.Count - _index - 1);
        }
        command.Execute();
        _stack.Add(command);
        _index = _stack.Count - 1;
        if (_stack.Count > limit)
        {
            _stack.RemoveAt(0);
            _index = _stack.Count - 1;
        }
        Notify();
    }

    /// <summary>
    /// Отменяет последнюю выполненную команду.
    /// </summary>
    public ICommand? Undo()
    {
        if (!CanUndo)
        {
            return null;
        }
        var command = _stack[_index];
        command.Undo();
        _index -= 1;
        Notify();
        return command;
    }

    /// <summary>
    /// Повторяет последнюю отмененную команду.
    /// </summary>
    public ICommand? Redo()
    {
        if (!CanRedo)
        {
            return null;
        }
        _index += 1;
        var command = _stack[_index];
        command.Execute();
        Notify();
        return command;
    }

    /// <summary>
    /// Очищает всю историю команд.
    /// </summary>
    public void Clear()
    {
        _stack.Clear();
        _index = -1;
        Notify();
    }

    private bool CanUndo => _index >= 0;
    private bool CanRedo => _index < _stack.Count - 1;

    private void Notify()
    {
        Emit(EventType.Change, new { CanUndo = CanUndo, CanRedo = CanRedo });
    }
}

