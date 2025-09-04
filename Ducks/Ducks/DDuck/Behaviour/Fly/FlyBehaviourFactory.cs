namespace Ducks.DDuck.Behaviour.Fly;

public static class FlyBehaviourFactory
{
    // Factory returns action and a bool value, 
    // that tells if it is a fly behaviour or not
    public static Tuple<Action, bool> FlyWithWings()
    {
        var behaviour =
            () => { Console.WriteLine("I'm flying with wings!!"); };
        return new Tuple<Action, bool>(behaviour, true);
    }

    public static Tuple<Action, bool> FlyNoWay()
    {
        var behaviour = () => { };
        return new Tuple<Action, bool>(behaviour, false);
    }
}