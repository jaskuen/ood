namespace RobotCommands.Lib.Commands.Implementation;

public class TurnOffCommand : ICommand
{
    private readonly Robot _robot;

    public TurnOffCommand(Robot robot)
    {
        _robot = robot;
    }

    public void Execute()
    {
        _robot.TurnOff();
    }
}