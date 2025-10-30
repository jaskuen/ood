namespace RobotCommands.Lib.Commands.Implementation;

public class MacroCommand : ICommand
{
    private readonly IList<ICommand> _commands;

    public MacroCommand(IList<ICommand> commands)
    {
        _commands = commands;
    }

    public void Execute()
    {
        foreach (ICommand cmd in _commands)
        {
            cmd.Execute();
        }
    }
}