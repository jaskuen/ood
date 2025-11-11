using Slides.Lib.Canvas;

namespace Slides.Lib.Shapes;

public class FillStyle
{
    public RgbaColor Color { get; set; }
    public bool IsEnabled { get; set; }

    public FillStyle()
    {
        Color = new RgbaColor(0, 0, 0, 1);
    }

    public FillStyle(bool isEnabled, RgbaColor color)
    {
        IsEnabled = isEnabled;
        Color = color;
    }
}