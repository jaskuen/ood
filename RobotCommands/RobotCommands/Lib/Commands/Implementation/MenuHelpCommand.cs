namespace RobotCommands.Lib.Commands.Implementation;

public class MenuHelpCommand : ICommand
{
    private readonly Menu _menu;

    public MenuHelpCommand(Menu menu)
    {
        _menu = menu;
    }

    public void Execute()
    {
        _menu.ShowInstructions();
    }
}