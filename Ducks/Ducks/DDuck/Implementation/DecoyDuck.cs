using Ducks.DDuck.Behaviour.Dance;
using Ducks.DDuck.Behaviour.Fly;
using Ducks.DDuck.Behaviour.Quack;

namespace Ducks.DDuck.Implementation;

public class DecoyDuck : Duck
{
    public DecoyDuck()
        : base(QuackBehaviourFactory.MuteQuackBehaviour(), FlyBehaviourFactory.FlyNoWay(),
            DanceBehaviourFactory.NoDance())
    {
    }

    public override void Display()
    {
        Console.WriteLine("I'm decoy duck!!");
    }
}