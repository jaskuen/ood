using Slides.Lib.Canvas;

namespace Slides.Lib.Shapes.Implementation;

public class Triangle : Shape
{
    private Point _point1;
    private Point _point2;
    private Point _point3;


    public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
    {
        _point1 = new Point(x1, y1);
        _point2 = new Point(x2, y2);
        _point3 = new Point(x3, y3);
    }

    protected override void DoDraw(ICanvas canvas)
    {
        if ((bool)FillStyle?.IsEnabled)
        {
            canvas.FillPolygon([_point1, _point2, _point3]);
        }

        if ((bool)LineStyle?.IsEnabled)
        {
            canvas.DrawLine(_point1, _point2);
            canvas.DrawLine(_point2, _point3);
            canvas.DrawLine(_point3, _point1);
        }
    }

    public override Frame GetFrame()
    {
        Point leftTop = new Point(
            Math.Min(Math.Min(_point1.X, _point2.X), _point3.X),
            Math.Min(Math.Min(_point1.Y, _point2.Y), _point3.Y));
        Point rightBottom = new Point(
            Math.Max(Math.Max(_point1.X, _point2.X), _point3.X),
            Math.Max(Math.Max(_point1.Y, _point2.Y), _point3.Y));

        return new Frame(leftTop.X, leftTop.Y, rightBottom.X, rightBottom.Y);
    }

    public override void SetFrame(double x1, double y1, double x2, double y2)
    {
        var oldFrame = GetFrame();
        double oldWidth = oldFrame.RightBottom.X - oldFrame.LeftTop.X;
        double oldHeight = oldFrame.RightBottom.Y - oldFrame.LeftTop.Y;

        if (oldWidth == 0 || oldHeight == 0)
        {
            return;
        }

        double newWidth = x2 - x1;
        double newHeight = y2 - y1;
        double scaleWidth = newWidth / oldWidth;
        double scaleHeight = newHeight / oldHeight;

        double newX1 = x1 + (_point1.X - oldFrame.LeftTop.X) * scaleWidth;
        double newY1 = y1 + (_point1.Y - oldFrame.LeftTop.Y) * scaleHeight;
        double newX2 = x1 + (_point2.X - oldFrame.LeftTop.X) * scaleWidth;
        double newY2 = y1 + (_point2.Y - oldFrame.LeftTop.Y) * scaleHeight;
        double newX3 = x1 + (_point3.X - oldFrame.LeftTop.X) * scaleWidth;
        double newY3 = y1 + (_point3.Y - oldFrame.LeftTop.Y) * scaleHeight;

        _point1 = new Point(newX1, newY1);
        _point2 = new Point(newX2, newY2);
        _point3 = new Point(newX3, newY3);
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