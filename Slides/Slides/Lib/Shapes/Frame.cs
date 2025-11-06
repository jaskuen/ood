using Slides.Lib.Canvas;

namespace Slides.Lib.Shapes;

public struct Frame
{
    public readonly Point LeftTop;
    public readonly Point RightBottom;

    public Frame(double x1, double y1, double x2, double y2)
    {
        LeftTop = new Point(x1, y1);
        RightBottom = new Point(x2, y2);
    }
}