using Ducks.DDuck.Behaviour.Dance.Implementation;
using Ducks.DDuck.Behaviour.Fly.Implementation;
using Ducks.DDuck.Behaviour.Quack.Implementation;

namespace Ducks.DDuck.Implementation;

public class DecoyDuck : Duck
{
    public DecoyDuck()
        : base(new MuteQuackBehaviour(), new FlyNoWay(), new NoDance())
    {
    }

    public override void Display()
    {
        Console.WriteLine("I'm decoy duck!!");
    }
}