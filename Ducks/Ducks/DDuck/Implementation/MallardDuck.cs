using Ducks.DDuck.Behaviour.Dance;
using Ducks.DDuck.Behaviour.Fly;
using Ducks.DDuck.Behaviour.Quack;

namespace Ducks.DDuck.Implementation;

public class MallardDuck : Duck
{
    public MallardDuck()
        : base(QuackBehaviourFactory.QuackBehaviour(), FlyBehaviourFactory.FlyWithWings(),
            DanceBehaviourFactory.Waltz())
    {
    }

    public override void Display()
    {
        Console.WriteLine("I'm mallard duck!!");
    }
}