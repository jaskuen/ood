using FiguresWithFactory.Lib.Canva;

namespace FiguresWithFactory.Lib.Shapes.Inheritors;

public class Rectangle : Shape
{
    private readonly Point _leftTop;
    private readonly Point _rightBottom;

    public Rectangle(Color color, double x1, double y1, double x2, double y2) : base(color)
    {
        _leftTop = new Point(x1, y1);
        _rightBottom = new Point(x2, y2);
    }

    protected override void DrawFigure(ICanvas canvas)
    {
        canvas.DrawLine(_leftTop.X, _leftTop.Y, _rightBottom.X, _leftTop.Y);
        canvas.DrawLine(_rightBottom.X, _leftTop.Y, _rightBottom.X, _rightBottom.Y);
        canvas.DrawLine(_rightBottom.X, _rightBottom.Y, _leftTop.X, _rightBottom.Y);
        canvas.DrawLine(_leftTop.X, _rightBottom.Y, _leftTop.X, _leftTop.Y);
    }
}