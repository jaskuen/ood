using Ducks.DDuck.Behaviour.Dance.Implementation;
using Ducks.DDuck.Behaviour.Fly.Implementation;
using Ducks.DDuck.Behaviour.Quack.Implementation;

namespace Ducks.DDuck.Implementation;

public class RubberDuck : Duck
{
    public RubberDuck()
        : base(new SqueakBehaviour(), new FlyNoWay(), new NoDance())
    {
    }

    public override void Display()
    {
        Console.WriteLine("I'm rubber duck!!");
    }
}