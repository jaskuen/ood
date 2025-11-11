using Slides.Lib.Canvas;

namespace Slides.Lib.Shapes.Implementation;

public class Ellipse : Shape
{
    private Point _center;
    private double _rX;
    private double _rY;

    public Ellipse(Point center, double rX, double rY)
    {
        _center = center;
        _rX = rX;
        _rY = rY;
    }

    protected override void DoDraw(ICanvas canvas)
    {
        if ((bool)FillStyle?.IsEnabled)
        {
            canvas.FillEllipse(_center, _rX, _rY);
        }

        if ((bool)LineStyle?.IsEnabled)
        {
            canvas.DrawEllipse(_center, _rX, _rY);
        }
    }

    public override Frame GetFrame()
    {
        return new Frame(
            _center.X - _rX,
            _center.Y - _rY,
            _center.X + _rX,
            _center.Y + _rY
        );
    }

    public override void SetFrame(double x1, double y1, double x2, double y2)
    {
        _rX = x2 - x1;
        _rY = y2 - y1;

        _center.X = x1 + _rX;
        _center.Y = y1 + _rY;
    }

    public override LineStyle? GetLineStyle() => LineStyle;

    public override FillStyle? GetFillStyle() => FillStyle;

    public override IShapes? GetShapesGroup()
    {
        return null;
    }

    protected override void DoSetLineStyleToChildren(bool isEnabled, RgbaColor color, double width = 1)
    {
    }

    protected override void DoSetFillStyleToChildren(bool isEnabled, RgbaColor color)
    {
    }
}