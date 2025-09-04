using Ducks.DDuck.Behaviour.Fly;

namespace Ducks.DDuck;

public interface IDuck
{
    public void Quack();
    public void Swim();
    public void Fly();
    public void Dance();
    public void Display();
    public bool IsQuackingOnEvenFlight();
    public void SetFlyBehaviour(Tuple<Action, bool> behaviour);
}