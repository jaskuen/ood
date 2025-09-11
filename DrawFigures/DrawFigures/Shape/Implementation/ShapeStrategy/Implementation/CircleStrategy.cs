using DrawFigures.Canvas;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public class CircleStrategy : IShapeStrategy
{
    public CircleStrategy(Point center, double radius)
    {
        Center = center;
        Radius = radius;
    }

    private Point Center { get; set; }
    private double Radius { get; set; }
    public void Draw(ICanvas canvas)
    {
        canvas.DrawEllipse(Center.X, Center.Y, Radius, Radius);
    }

    public void MoveTo(double x, double y)
    {
        Center.X += x;
        Center.Y += y;
    }
}