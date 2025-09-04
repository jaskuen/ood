using Ducks.DDuck.Behaviour.Dance;
using Ducks.DDuck.Behaviour.Fly;
using Ducks.DDuck.Behaviour.Quack;

namespace Ducks.DDuck.Implementation;

public class RedheadDuck : Duck
{
    public RedheadDuck()
        : base(QuackBehaviourFactory.QuackBehaviour(), FlyBehaviourFactory.FlyWithWings(),
            DanceBehaviourFactory.Minuet())
    {
    }

    public override void Display()
    {
        Console.WriteLine("I'm redhead duck!!");
    }
}