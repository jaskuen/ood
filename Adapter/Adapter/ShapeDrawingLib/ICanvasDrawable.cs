using Adapter.GraphicsLib;

namespace Adapter.ShapeDrawingLib;

public struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public interface ICanvasDrawable
{
    public void Draw(ICanvas canvas);
}