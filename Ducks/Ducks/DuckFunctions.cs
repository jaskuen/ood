using Ducks.DDuck;

namespace Ducks;

public static class DuckFunctions
{
    public static void DrawDuck(IDuck duck)
    {
        duck.Display();
    }

    public static void PlayWithDuck(IDuck duck)
    {
        DrawDuck(duck);
        duck.Quack();
        duck.Fly();
        duck.Dance();
        Console.WriteLine();
    }
}