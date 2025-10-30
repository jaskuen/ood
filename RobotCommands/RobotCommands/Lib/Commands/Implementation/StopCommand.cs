namespace RobotCommands.Lib.Commands.Implementation;

public class StopCommand : ICommand
{
    private readonly Robot _robot;

    public StopCommand(Robot robot)
    {
        _robot = robot;
    }

    public void Execute()
    {
        _robot.Stop();
    }
}