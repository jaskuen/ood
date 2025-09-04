using Ducks.DDuck.Behaviour.Dance.Implementation;
using Ducks.DDuck.Behaviour.Fly.Implementation;
using Ducks.DDuck.Behaviour.Quack.Implementation;

namespace Ducks.DDuck.Implementation;

public class RedheadDuck : Duck
{
    public RedheadDuck()
        : base(new QuackBehaviour(), new FlyWithWings(), new Minuet())
    {
    }

    public override void Display()
    {
        Console.WriteLine("I'm redhead duck!!");
    }
}