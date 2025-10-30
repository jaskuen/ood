namespace RobotCommands.Lib.Commands.Implementation;

public class WalkCommand : ICommand
{
    private readonly Robot _robot;
    private readonly WalkDirection _direction;

    public WalkCommand(Robot robot, WalkDirection direction)
    {
        _robot = robot;
        _direction = direction;
    }

    public void Execute()
    {
        _robot.Walk(_direction);
    }
}