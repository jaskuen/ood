namespace Adapter.GraphicsLib.Implementation;

public class Canvas : ICanvas
{
    public void MoveTo(int x, int y)
    {
        Console.WriteLine($"MoveTo ({x}, {y})");
    }

    public void LineTo(int x, int y)
    {
        Console.WriteLine($"LineTo ({x}, {y})");
    }
}