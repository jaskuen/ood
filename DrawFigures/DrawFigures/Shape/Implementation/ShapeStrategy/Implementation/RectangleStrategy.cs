using DrawFigures.Canvas;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public class RectangleStrategy : IShapeStrategy
{
    public RectangleStrategy(float pointX, float pointY, float width, float height)
    {
        Point = new Point(pointX, pointY);
        Width = width;
        Height = height;
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

    public string StringParams() => $"rectangle {Point.X} {Point.Y} {Width} {Height}";
}