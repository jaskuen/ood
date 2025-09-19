using DrawFigures.Canvas;
using DrawFigures.Picture.Implementation;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public class TriangleStrategy : IShapeStrategy
{
    public TriangleStrategy(string x1, string y1, string x2, string y2, string x3, string y3)
    {
        Point1 = new Point(x1.ToFloat(), y1.ToFloat());
        Point2 = new Point(x2.ToFloat(), y2.ToFloat());
        Point3 = new Point(x3.ToFloat(), y3.ToFloat());
    }

    private Point Point1 { get; set; }
    private Point Point2 { get; set; }
    private Point Point3 { get; set; }

    public void Draw(ICanvas canvas)
    {
        canvas.MoveTo(Point1.X, Point1.Y);
        canvas.LineTo(Point2.X, Point2.Y);
        canvas.LineTo(Point3.X, Point3.Y);
        canvas.LineTo(Point1.X, Point1.Y);
    }

    public void MoveBy(float x, float y)
    {
        Point1.X += x;
        Point1.Y += y;
        Point2.X += x;
        Point2.Y += y;
        Point3.X += x;
        Point3.Y += y;
    }

    public string StringParams() => $"triangle {Point1.X} {Point1.Y} {Point2.X} {Point2.Y} {Point3.X} {Point3.Y}";
}