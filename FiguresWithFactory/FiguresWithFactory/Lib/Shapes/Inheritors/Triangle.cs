using FiguresWithFactory.Lib.Canva;

namespace FiguresWithFactory.Lib.Shapes.Inheritors;

public class Triangle : Shape
{
    private readonly Point _point1;
    private readonly Point _point2;
    private readonly Point _point3;

    public Triangle(Color color, double x1, double y1, double x2, double y2, double x3, double y3) : base(color)
    {
        _point1 = new Point(x1, y1);
        _point2 = new Point(x2, y2);
        _point3 = new Point(x3, y3);
    }

    protected override void DrawFigure(ICanvas canvas)
    {
        canvas.DrawLine(_point1.X, _point1.Y, _point2.X, _point2.Y);
        canvas.DrawLine(_point2.X, _point2.Y, _point3.X, _point3.Y);
        canvas.DrawLine(_point3.X, _point3.Y, _point1.X, _point1.Y);
    }
}