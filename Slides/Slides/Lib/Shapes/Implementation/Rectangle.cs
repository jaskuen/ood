using Slides.Lib.Canvas;

namespace Slides.Lib.Shapes.Implementation;

public class Rectangle : Shape
{
    private Point _leftTop;
    private Point _rightTop;
    private Point _rightBottom;
    private Point _leftBottom;

    public Rectangle(double x1, double y1, double x2, double y2)
    {
        _leftTop = new Point(x1, y1);
        _rightTop = new Point(x2, y1);
        _rightBottom = new Point(x2, y2);
        _leftBottom = new Point(x1, y2);
    }

    protected override void DoDraw(ICanvas canvas)
    {
        if ((bool)FillStyle?.IsEnabled)
        {
            canvas.FillPolygon([_leftTop, _rightTop, _rightBottom, _leftBottom]);
        }

        if ((bool)LineStyle?.IsEnabled)
        {
            canvas.DrawLine(_leftTop, _rightTop);
            canvas.DrawLine(_rightTop, _rightBottom);
            canvas.DrawLine(_rightBottom, _leftBottom);
            canvas.DrawLine(_leftBottom, _leftTop);
        }
    }

    public override Frame GetFrame()
    {
        return new Frame(_leftTop.X, _leftTop.Y, _rightBottom.X, _rightBottom.Y);
    }

    public override void SetFrame(double x1, double y1, double x2, double y2)
    {
        _leftTop = new Point(x1, y1);
        _rightTop = new Point(x2, y1);
        _rightBottom = new Point(x2, y2);
        _leftBottom = new Point(x1, y2);
    }

    public override LineStyle? GetLineStyle() => LineStyle;

    public override FillStyle? GetFillStyle() => FillStyle;

    protected override void DoSetLineStyleToChildren(bool isEnabled, RgbaColor color, double width = 1)
    {
    }

    protected override void DoSetFillStyleToChildren(bool isEnabled, RgbaColor color)
    {
    }

    public override IShapes? GetShapesGroup()
    {
        return null;
    }
}