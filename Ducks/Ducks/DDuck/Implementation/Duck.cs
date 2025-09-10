using Ducks.DDuck.Behaviour.Dance;
using Ducks.DDuck.Behaviour.Fly;
using Ducks.DDuck.Behaviour.Fly.Implementation;
using Ducks.DDuck.Behaviour.Quack;
using Ducks.DDuck.Behaviour.Quack.Implementation;

namespace Ducks.DDuck.Implementation;

public class Duck : IDuck
{
    private readonly IQuackBehaviour _quackBehaviour;
    private readonly IDanceBehaviour _danceBehaviour;
    private IFlyBehaviour _flyBehaviour;

    private bool _quackOnEvenFlight = false;
    private int _fliesCount = 0;

    private bool CanFly()
    {
        return _flyBehaviour.GetType() != typeof(FlyNoWay);
    }

    private void OnFly()
    {
        int num = _quackOnEvenFlight ? 0 : 1;
        if (++_fliesCount % 2 == num)
        {
            Quack();
        }
    }

    public Duck(IQuackBehaviour quackBehaviour, IFlyBehaviour flyBehaviour, IDanceBehaviour danceBehaviour)
    {
        _quackBehaviour = quackBehaviour;
        _flyBehaviour = flyBehaviour;
        _danceBehaviour = danceBehaviour;
    }

    public void Quack()
    {
        _quackBehaviour.Quack();
    }

    public void Swim()
    {
        Console.WriteLine("Swimming!!");
    }

    public void Fly()
    {
        if (CanFly())
        {
            OnFly();
            Console.WriteLine($"This will be my {_fliesCount} flight!!");
        }

        _flyBehaviour.Fly();
    }

    public void Dance()
    {
        _danceBehaviour.Dance();
    }

    public virtual void Display()
    {
    }

    public bool IsQuackingOnEvenFlight() => _quackOnEvenFlight;

    public void SetFlyBehaviour(IFlyBehaviour behaviour)
    {
        _flyBehaviour = behaviour;
    }
}