using Slides.Lib.Canvas;

namespace Slides.Lib.Shapes;

public class FillStyle
{
    public RgbaColor Color { get; set; }

    public FillStyle()
    {
        Color = new RgbaColor(0, 0, 0, 1);
    }
}