using Ducks.DDuck.Behaviour.Dance.Implementation;
using Ducks.DDuck.Behaviour.Fly.Implementation;
using Ducks.DDuck.Behaviour.Quack.Implementation;

namespace Ducks.DDuck.Implementation;

public class ModelDuck : Duck
{
    public ModelDuck()
        : base(new QuackBehaviour(), new FlyNoWay(), new NoDance())
    {
    }

    public override void Display()
    {
        Console.WriteLine("I'm model duck!!");
    }
}