using RobotCommands.Lib.Commands;

namespace RobotCommands.Lib.Commands.Implementation;

public class TurnOnCommand : ICommand
{
    private readonly Robot _robot;

    public TurnOnCommand(Robot robot)
    {
        _robot = robot;
    }

    public void Execute()
    {
        _robot.TurnOn();
    }
}