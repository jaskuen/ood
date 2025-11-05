namespace RobotCommands.Lib.Commands.Implementation;

public class BeginMacroCommand : ICommand
{
    private readonly Menu _menu;

    public BeginMacroCommand(Menu menu)
    {
        _menu = menu;
    }

    public void Execute()
    {
        Console.WriteLine("Starting to read macro command");
        _menu.StartMacro();
    }
}