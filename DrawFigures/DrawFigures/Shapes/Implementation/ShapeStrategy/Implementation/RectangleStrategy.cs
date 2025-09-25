using DrawFigures.Canvas;
using DrawFigures.Picture.Implementation;

namespace DrawFigures.Shapes.Implementation.ShapeStrategy.Implementation;

public class RectangleStrategy : IShapeStrategy
{
    public RectangleStrategy(string pointX, string pointY, string width, string height)
    {
        Point = new Point(pointX.ToFloat(), pointY.ToFloat());
        Width = width.ToFloat();
        Height = height.ToFloat();
    }

    private Point Point { get; set; }
    private float Width { get; set; }
    private float Height { get; set; }

    public void Draw(ICanvas canvas)
    {
        canvas.DrawRect(Point.X, Point.Y, Width, Height);
    }

    public void MoveBy(float x, float y)
    {
        Point.X += x;
        Point.Y += y;
    }

    public string StringParams()
    {
        return $"rectangle {Point.X} {Point.Y} {Width} {Height}";
    }
}