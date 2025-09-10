namespace Ducks.DDuck.Implementation;

public class Duck : IDuck
{
    private readonly Action _quackBehaviour;
    private readonly Action _danceBehaviour;
    private Action _flyBehaviour;

    private bool _canFly = false;
    private readonly bool _quackOnEvenFlight = false;
    private int _fliesCount = 0;

    private void OnFly()
    {
        var num = _quackOnEvenFlight ? 0 : 1;
        if (++_fliesCount % 2 == num) Quack();
    }

    public Duck(Action quackBehaviour, Tuple<Action, bool> flyBehaviour, Action danceBehaviour)
    {
        _quackBehaviour = quackBehaviour;
        _flyBehaviour = flyBehaviour.Item1;
        _danceBehaviour = danceBehaviour;
        _canFly = flyBehaviour.Item2;
    }

    public void Quack()
    {
        _quackBehaviour();
    }

    public void Swim()
    {
        Console.WriteLine("Swimming!!");
    }

    public void Fly()
    {
        if (_canFly)
        {
            OnFly();
            Console.WriteLine($"This will be my {_fliesCount} flight!!");
        }

        _flyBehaviour();
    }

    public void Dance()
    {
        _danceBehaviour();
    }

    public virtual void Display()
    {
    }

    public bool IsQuackingOnEvenFlight()
    {
        return _quackOnEvenFlight;
    }

    public void SetFlyBehaviour(Tuple<Action, bool> behaviour)
    {
        _flyBehaviour = behaviour.Item1;
        _canFly = behaviour.Item2;
    }
}