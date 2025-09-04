namespace Ducks.DDuck.Behaviour.Dance;

public interface DanceBehaviourFactory
{
    public static Action NoDance()
    {
        var behaviour = () => { };
        return behaviour;
    }

    public static Action Waltz()
    {
        var behaviour = () => { Console.WriteLine("Waltz"); };
        return behaviour;
    }

    public static Action Minuet()
    {
        var behaviour = () => { Console.WriteLine("Minuet"); };
        return behaviour;
    }
}