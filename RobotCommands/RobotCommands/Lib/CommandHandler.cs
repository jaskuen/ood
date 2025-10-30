using RobotCommands.Lib.Commands;
using RobotCommands.Lib.Commands.Implementation;

namespace RobotCommands.Lib;

public class CommandHandler
{
    private readonly Menu _menu;

    public CommandHandler(Robot robot, Menu menu)
    {
        _menu = menu;

        robot = robot ?? throw new ArgumentNullException(nameof(robot));
        _menu = _menu ?? throw new ArgumentNullException(nameof(_menu));

        // Basic commands
        _menu.AddItem("on", "Turns the Robot on", new TurnOnCommand(robot));
        _menu.AddItem("off", "Turns the Robot off", new TurnOffCommand(robot));
        _menu.AddItem("stop", "Stops the Robot", new StopCommand(robot));

        // Directional walk commands
        _menu.AddItem("north", "Makes the Robot walk north", new WalkCommand(robot, WalkDirection.North));
        _menu.AddItem("south", "Makes the Robot walk south", new WalkCommand(robot, WalkDirection.South));
        _menu.AddItem("west", "Makes the Robot walk west", new WalkCommand(robot, WalkDirection.West));
        _menu.AddItem("east", "Makes the Robot walk east", new WalkCommand(robot, WalkDirection.East));

        // Macro command create user commands
        _menu.AddItem("begin_macro", "Starts macro command creation process", new BeginMacroCommand(_menu));
        _menu.AddItem("end_macro", "Ends macro command creation process", new EndMacroCommand(_menu));

        // Macro command: patrol
        IList<ICommand> patrol = new List<ICommand>();
        patrol.Add(new TurnOnCommand(robot));
        patrol.Add(new WalkCommand(robot, WalkDirection.North));
        patrol.Add(new WalkCommand(robot, WalkDirection.East));
        patrol.Add(new WalkCommand(robot, WalkDirection.South));
        patrol.Add(new WalkCommand(robot, WalkDirection.West));
        patrol.Add(new TurnOffCommand(robot));
        _menu.AddItem("patrol", "Patrol the territory", new MacroCommand(patrol));

        // _menu controls
        _menu.AddItem("help", "Show instructions", new MenuHelpCommand(_menu));
        _menu.AddItem("exit", "Exit from this menu", new ExitMenuCommand(_menu));
    }

    public void Run()
    {
        _menu.Run();
    }
}