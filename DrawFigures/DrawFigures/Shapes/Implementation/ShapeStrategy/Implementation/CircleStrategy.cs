using DrawFigures.Canvas;
using DrawFigures.Picture.Implementation;

namespace DrawFigures.Shapes.Implementation.ShapeStrategy.Implementation;

public class CircleStrategy : IShapeStrategy
{
    public CircleStrategy(string centerX, string centerY, string radius)
    {
        Center = new Point(centerX.ToFloat(), centerY.ToFloat());
        Radius = radius.ToFloat();
    }

    private Point Center { get; }
    private float Radius { get; }

    public void Draw(ICanvas canvas)
    {
        canvas.DrawEllipse(Center.X, Center.Y, Radius, Radius);
    }

    public void MoveBy(float x, float y)
    {
        Center.X += x;
        Center.Y += y;
    }

    public string StringParams()
    {
        return $"circle {Center.X} {Center.Y} {Radius}";
    }
}