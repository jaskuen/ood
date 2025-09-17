using DrawFigures.Canvas;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public class LineStrategy : IShapeStrategy
{
    public LineStrategy(float startX, float startY, float endX, float endY)
    {
        StartPoint = new Point(startX, startY);
        EndPoint = new Point(endX, endY);
    }

    private Point StartPoint { get; set; }
    private Point EndPoint { get; set; }

    public void Draw(ICanvas canvas)
    {
        canvas.MoveTo(StartPoint.X, StartPoint.Y);
        canvas.LineTo(EndPoint.X, EndPoint.Y);
    }

    public void MoveBy(float x, float y)
    {
        StartPoint.X += x;
        StartPoint.Y += y;
        EndPoint.X += x;
        EndPoint.Y += y;
    }

    public string StringParams() =>  $"line {StartPoint.X} {StartPoint.Y} {EndPoint.X} {EndPoint.Y}";
}