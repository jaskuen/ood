using DrawFigures.Canvas;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public class RectangleStrategy : IShapeStrategy
{
    public RectangleStrategy(Point point, double width, double height)
    {
        Point = point;
        Width = width;
        Height = height;
    }

    private Point Point { get; set; }
    private double Width { get; set; }
    private double Height { get; set; }
    
    public void Draw(ICanvas canvas)
    {
        canvas.MoveTo(Point.X, Point.Y);
        canvas.LineTo(Point.X + Width, Point.Y);
        canvas.LineTo(Point.X + Width, Point.Y + Height);
        canvas.LineTo(Point.X, Point.Y + Height);
        canvas.LineTo(Point.X, Point.Y);
    }

    public void MoveTo(double x, double y)
    {
        Point.X += x;
        Point.Y += y;
    }
}