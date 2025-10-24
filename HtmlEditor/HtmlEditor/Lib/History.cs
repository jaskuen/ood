using HtmlEditor.Lib.Command;

namespace HtmlEditor.Lib;

public class History
{
    private const int MaxHistorySize = 10;

    private readonly List<ICommand> _commands = [];
    private int _currentActionIndex = -1;

    public void AddAndExecuteCommand(ICommand command)
    {
        if (_commands.Count > 0 
            && _currentActionIndex >= 0 
            && _commands[_currentActionIndex].Merge(command))
        {
            return;
        }

        AddNewCommand(command);
        _currentActionIndex++;
        command.Execute();
    }

    public bool CanUndo() => _currentActionIndex >= 0;

    public bool CanRedo() => _currentActionIndex < _commands.Count && _commands.Count > 0;

    public void Undo()
    {
        if (CanUndo())
        {
            _commands[_currentActionIndex--].Undo();
        }
    }

    public void Redo()
    {
        if (CanRedo())
        {
            _commands[++_currentActionIndex].Execute();
        }
    }

    private void AddNewCommand(ICommand command)
    {
        if (_currentActionIndex < _commands.Count - 1)
        {
            for (int i = _commands.Count - 1; i > _currentActionIndex; i--)
            {
                _commands[i].Destroy();
            }

            _commands.RemoveRange(_currentActionIndex + 1, _commands.Count - _currentActionIndex - 1);
        }

        if (_commands.Count == MaxHistorySize)
        {
            _commands.RemoveAt(0);
            _currentActionIndex--;
        }

        _commands.Add(command);
    }
}