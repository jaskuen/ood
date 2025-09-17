using DrawFigures.Canvas;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public class CircleStrategy : IShapeStrategy
{
    public CircleStrategy(float centerX, float centerY, float radius)
    {
        Center = new Point(centerX, centerY);
        Radius = radius;
    }

    private Point Center { get; }
    private float Radius { get; }

    public void Draw(ICanvas canvas)
    {
        canvas.DrawEllipse(Center.X, Center.Y, Radius);
    }

    public void MoveBy(float x, float y)
    {
        Center.X += x;
        Center.Y += y;
    }

    public string StringParams() => $"circle {Center.X} {Center.Y} {Radius}";
}