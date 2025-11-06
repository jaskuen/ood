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

    public void SetColor(int color)
    {
        int red = (color >> 16) & 0xFF;
        int green = (color >> 8) & 0xFF;
        int blue = color & 0xFF;

        Console.WriteLine($"SetColor (#{red:X2}{green:X2}{blue:X2})");
    }
}