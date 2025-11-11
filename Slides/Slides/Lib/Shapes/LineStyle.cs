using Slides.Lib.Canvas;

namespace Slides.Lib.Shapes;

public class LineStyle
{
    public RgbaColor Color { get; set; }
    public double Width { get; set; }
    public bool IsEnabled { get; set; }

    public LineStyle()
    {
        Color = new RgbaColor(0, 0, 0, 1);
        Width = 1;
    }

    public LineStyle(bool isEnabled, RgbaColor color, double width)
    {
        IsEnabled = isEnabled;
        Color = color;
        Width = width;
    }
}