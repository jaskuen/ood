using Ducks.DDuck.Behaviour.Dance;
using Ducks.DDuck.Behaviour.Fly;
using Ducks.DDuck.Behaviour.Quack;

namespace Ducks.DDuck.Implementation;

public class RubberDuck : Duck
{
    public RubberDuck()
        : base(QuackBehaviourFactory.SqueakBehaviour(), FlyBehaviourFactory.FlyNoWay(), DanceBehaviourFactory.NoDance())
    {
    }

    public override void Display()
    {
        Console.WriteLine("I'm rubber duck!!");
    }
}