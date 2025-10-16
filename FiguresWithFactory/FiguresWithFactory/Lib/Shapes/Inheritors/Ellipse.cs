using FiguresWithFactory.Lib.Canva;

namespace FiguresWithFactory.Lib.Shapes.Inheritors;

public class Ellipse : Shape
{
    private readonly Point _center;
    private readonly double _radiusX;
    private readonly double _radiusY;

    public Ellipse(Color color, double cx, double cy, double rx, double ry) : base(color)
    {
        _center = new Point(cx, cy);
        _radiusX = rx;
        _radiusY = ry;
    }

    protected override void DrawFigure(ICanvas canvas)
    {
        canvas.DrawEllipse(_center.X, _center.Y, _radiusX, _radiusY);
    }
}