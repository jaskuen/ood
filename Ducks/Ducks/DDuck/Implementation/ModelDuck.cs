using Ducks.DDuck.Behaviour.Dance;
using Ducks.DDuck.Behaviour.Fly;
using Ducks.DDuck.Behaviour.Quack;

namespace Ducks.DDuck.Implementation;

public class ModelDuck : Duck
{
    public ModelDuck()
        : base(QuackBehaviourFactory.QuackBehaviour(), FlyBehaviourFactory.FlyNoWay(), DanceBehaviourFactory.NoDance())
    {
    }

    public override void Display()
    {
        Console.WriteLine("I'm model duck!!");
    }
}