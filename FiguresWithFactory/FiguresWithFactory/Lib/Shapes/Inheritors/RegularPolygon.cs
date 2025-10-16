using FiguresWithFactory.Lib.Canva;

namespace FiguresWithFactory.Lib.Shapes.Inheritors;

public class RegularPolygon : Shape
{
    private readonly int _vertexCount;
    private readonly double _radius;
    private readonly Point _center;
    
    public RegularPolygon(Color color, double cx, double cy, int vertexCount, double radius) : base(color)
    {
        _center = new Point(cx, cy);
        _vertexCount = vertexCount;
        _radius = radius;
    }

    protected override void DrawFigure(ICanvas canvas)
    {
        if (_vertexCount < 3)
        {
            throw new Exception("There must be at least three vertices");
        }

        double angleStep = 2 * Math.PI / _vertexCount;
        double startAngle = -Math.PI / 2 + (_vertexCount % 2 == 0 ? angleStep / 2 : 0);

        double prevX = _center.X + _radius * Math.Cos(startAngle);
        double prevY = _center.Y + _radius * Math.Sin(startAngle);

        for (int i = 1; i <= _vertexCount; i++)
        {
            double angle = startAngle + i * angleStep;
            double x = _center.X + _radius * Math.Cos(angle);
            double y = _center.Y + _radius * Math.Sin(angle);

            canvas.DrawLine(prevX, prevY, x, y);

            prevX = x;
            prevY = y;
        }
    }
}