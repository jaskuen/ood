using DrawFigures.Canvas;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public class TriangleStrategy : IShapeStrategy
{
    public TriangleStrategy(double x1, double y1, double x2, double y2, double x3, double y3)
    {
        Point1 = new Point(x1, y1);
        Point2 = new Point(x2, y2);
        Point3 = new Point(x3, y3);
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

    public void MoveTo(double x, double y)
    {
        throw new NotImplementedException();
    }
}