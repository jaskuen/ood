namespace Ducks.DDuck.Behaviour.Quack;

public static class QuackBehaviourFactory
{
    public static Action MuteQuackBehaviour()
    {
        var behaviour = () => { };
        return behaviour;
    }

    public static Action QuackBehaviour()
    {
        var behaviour = () => { Console.WriteLine("Quack quack!!"); };
        return behaviour;
    }

    public static Action SqueakBehaviour()
    {
        var behaviour = () => { Console.WriteLine("Squeak!!"); };
        return behaviour;
    }
}