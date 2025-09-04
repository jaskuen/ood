using Ducks.DDuck.Behaviour.Dance.Implementation;
using Ducks.DDuck.Behaviour.Fly.Implementation;
using Ducks.DDuck.Behaviour.Quack.Implementation;

namespace Ducks.DDuck.Implementation;

public class MallardDuck : Duck
{
    public MallardDuck()
        : base(new QuackBehaviour(), new FlyWithWings(), new Waltz())
    {
    }

    public override void Display()
    {
        Console.WriteLine("I'm mallard duck!!");
    }
}