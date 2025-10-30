using RobotCommands.Lib.Commands;
using RobotCommands.Lib.Commands.Implementation;

namespace RobotCommands.Lib;

public class Menu
{
    private readonly List<MenuItem> _items = new();
    private bool _exit;
    private bool _macroWriteMode;
    private readonly List<ICommand> _macroCommandsToAdd = new();
    private string _macroShortcut = String.Empty;
    private string _macroDescription = String.Empty;

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

    public void Exit()
    {
        _exit = true;
    }

    public void StartMacroWriteMode()
    {
        _macroWriteMode = true;
        _macroCommandsToAdd.Clear();

        ReadMacroShortcutAndDescription();
    }

    public bool EndMacroWriteModeAndSaveCommand()
    {
        _macroWriteMode = false;

        if (_macroCommandsToAdd.Count == 0)
        {
            Console.WriteLine("Empty macro, not saving");
            return false;
        }

        ICommand macroCommand = new MacroCommand(_macroCommandsToAdd);
        AddItem(_macroShortcut, _macroDescription, macroCommand);
        return true;
    }

    private bool ExecuteCommand(string command)
    {
        _exit = false;

        ICommand? itemCommand = _items.FirstOrDefault(i => i.Shortcut == command)?.Command;
        if (itemCommand != null)
        {
            if (_macroWriteMode)
            {
                switch (TryAddCommandToMacro(itemCommand))
                {
                    case AddCommandToMacroResult.Added:
                    case AddCommandToMacroResult.Error:
                        return true;
                    case AddCommandToMacroResult.FinishMacro:
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

    private void ReadMacroShortcutAndDescription()
    {
        Console.WriteLine("Type your macro command shortcut:");
        do
        {
            Console.Write("> ");
        } while (string.IsNullOrWhiteSpace(_macroShortcut = Console.ReadLine() ?? string.Empty) &&
                 CheckMacroName(_macroShortcut));

        Console.WriteLine("Type your macro command description:");
        do
        {
            Console.Write("> ");
        } while (string.IsNullOrWhiteSpace(_macroDescription = Console.ReadLine() ?? string.Empty));
    }

    private bool CheckMacroName(string macroName)
    {
        return _items.Any(i => i.Shortcut == macroName);
    }

    private AddCommandToMacroResult TryAddCommandToMacro(ICommand command)
    {
        if (command is BeginMacroCommand)
        {
            Console.WriteLine("Cannot create another macro inside macro");
            return AddCommandToMacroResult.Error;
        }

        if (command is EndMacroCommand)
        {
            return AddCommandToMacroResult.FinishMacro;
        }

        _macroCommandsToAdd.Add(command);
        return AddCommandToMacroResult.Added;
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

    private enum AddCommandToMacroResult
    {
        Error,
        FinishMacro,
        Added
    }
}