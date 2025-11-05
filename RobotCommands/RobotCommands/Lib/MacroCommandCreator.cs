using RobotCommands.Lib.Commands;
using RobotCommands.Lib.Commands.Implementation;

namespace RobotCommands.Lib;

public class MacroCommandCreator
{
    private IList<string> _shortcuts = new List<string>();

    private bool _macroWriteMode = false;
    private readonly List<ICommand> _macroCommandsToAdd = new();
    private string _macroShortcut = String.Empty;
    private string _macroDescription = String.Empty;

    public bool IsMacroWriteMode() => _macroWriteMode;

    public string MacroShortcut() => _macroShortcut;
    public string MacroDescription() => _macroDescription;

    public void StartMacroWriteMode(IList<string> shortcuts)
    {
        _shortcuts = shortcuts;

        _macroWriteMode = true;
        _macroCommandsToAdd.Clear();

        ReadMacroShortcutAndDescription();
    }

    public ICommand? EndMacroWriteModeAndSaveCommand()
    {
        _macroWriteMode = false;

        if (_macroCommandsToAdd.Count == 0)
        {
            Console.WriteLine("Empty macro, not saving");
            return null;
        }

        return new MacroCommand(_macroCommandsToAdd);
    }


    private void ReadMacroShortcutAndDescription()
    {
        Console.WriteLine("Type your macro command shortcut:");
        do
        {
            Console.Write("> ");
        } while (string.IsNullOrWhiteSpace(_macroShortcut = Console.ReadLine() ?? string.Empty) ||
                 CheckMacroName(_macroShortcut));

        Console.WriteLine("Type your macro command description:");
        do
        {
            Console.Write("> ");
        } while (string.IsNullOrWhiteSpace(_macroDescription = Console.ReadLine() ?? string.Empty));
    }

    public AddCommandToMacroResult TryAddCommandToMacro(ICommand command)
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

    private bool CheckMacroName(string macroName)
    {
        if (!_shortcuts.Contains(macroName))
        {
            return true;
        }

        // Почему функция выводит в конслось
        Console.WriteLine($"{macroName} shortcut already exists");
        return false;
    }


    public enum AddCommandToMacroResult
    {
        Error,
        FinishMacro,
        Added
    }
}