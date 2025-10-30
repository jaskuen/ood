namespace RobotCommands.Lib.Commands.Implementation;

public class ExitMenuCommand : ICommand
{
    private readonly Menu _menu;

    public ExitMenuCommand(Menu menu)
    {
        _menu = menu;
    }

    public void Execute()
    {
        _menu.Exit();
    }
}