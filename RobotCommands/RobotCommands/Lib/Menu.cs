using RobotCommands.Lib.Commands;
using RobotCommands.Lib.Commands.Implementation;

namespace RobotCommands.Lib;

public class Menu
{
    private readonly List<MenuItem> _items = new();
    private bool _exit;

    private MacroCommandCreator _macroCommandCreator = new();

    public void AddItem(string shortcut, string description, ICommand command)
    {
        _items.Add(new MenuItem(shortcut, description, command));
    }

    public void Run()
    {
        ShowInstructions();

        string command;
        do
        {
            Console.Write("> ");
        } while ((command = Console.ReadLine() ?? string.Empty) is not null && ExecuteCommand(command));
    }

    public void ShowInstructions()
    {
        Console.WriteLine("Commands list:");
        foreach (var item in _items)
        {
            Console.WriteLine($"  {item.Shortcut}: {item.Description}");
        }
    }

    public void StartMacro()
    {
        _macroCommandCreator.StartMacroWriteMode(_items.Select(i => i.Shortcut).ToList());
    }

    public bool AddMacroCommand()
    {
        ICommand? command = _macroCommandCreator.EndMacroWriteModeAndSaveCommand();
        if (command is null)
        {
            return false;
        }

        AddItem(_macroCommandCreator.MacroShortcut(), _macroCommandCreator.MacroDescription(), command);
        return true;
    }

    public void Exit()
    {
        _exit = true;
    }

    private bool ExecuteCommand(string command)
    {
        _exit = false;

        ICommand? itemCommand = _items.FirstOrDefault(i => i.Shortcut == command)?.Command;
        if (itemCommand != null)
        {
            if (_macroCommandCreator.IsMacroWriteMode())
            {
                switch (_macroCommandCreator.TryAddCommandToMacro(itemCommand))
                {
                    case MacroCommandCreator.AddCommandToMacroResult.Added:
                    case MacroCommandCreator.AddCommandToMacroResult.Error:
                        return true;
                    case MacroCommandCreator.AddCommandToMacroResult.FinishMacro:
                        break;
                }
            }

            itemCommand.Execute();
        }
        else
        {
            Console.WriteLine("Unknown command");
        }

        return !_exit;
    }

    private class MenuItem
    {
        public string Shortcut { get; }
        public string Description { get; }
        public ICommand Command { get; }

        public MenuItem(string shortcut, string description, ICommand command)
        {
            Shortcut = shortcut;
            Description = description;
            Command = command;
        }
    }
}