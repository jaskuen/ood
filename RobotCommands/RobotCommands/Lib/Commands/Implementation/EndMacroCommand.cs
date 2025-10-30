namespace RobotCommands.Lib.Commands.Implementation;

public class EndMacroCommand : ICommand
{
    private readonly Menu _menu;

    public EndMacroCommand(Menu menu)
    {
        _menu = menu;
    }

    public void Execute()
    {
        if (_menu.EndMacroWriteModeAndSaveCommand())
        {
            Console.WriteLine("Saved macro command");
        }
    }
}