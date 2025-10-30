namespace RobotCommands.Lib;

public enum WalkDirection
{
    North,
    South,
    West,
    East
}

public class Robot
{
    private bool _turnedOn = false;
    private WalkDirection? _direction = null;

    private static readonly Dictionary<WalkDirection, string> DirectionToString = new()
    {
        { WalkDirection.East, "east" },
        { WalkDirection.South, "south" },
        { WalkDirection.West, "west" },
        { WalkDirection.North, "north" }
    };

    public void TurnOn()
    {
        if (!_turnedOn)
        {
            _turnedOn = true;
            Console.WriteLine("It am waiting for your commands");
        }
    }

    public void TurnOff()
    {
        if (_turnedOn)
        {
            _turnedOn = false;
            _direction = null;
            Console.WriteLine("It is a pleasure to serve you");
        }
    }

    public void Walk(WalkDirection direction)
    {
        if (_turnedOn)
        {
            _direction = direction;
            Console.WriteLine($"Walking {DirectionToString[direction]}");
        }
        else
        {
            Console.WriteLine("The robot should be turned on first");
        }
    }

    public void Stop()
    {
        if (_turnedOn)
        {
            if (_direction.HasValue)
            {
                _direction = null;
                Console.WriteLine("Stopped");
            }
            else
            {
                Console.WriteLine("I am staying still");
            }
        }
        else
        {
            Console.WriteLine("The robot should be turned on first");
        }
    }
}