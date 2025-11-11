using Slides.Lib.Canvas;
using Slides.Lib.Shapes;

namespace Slides.Lib.Extensions;

public static class ComparisonExtensions
{
    private const double DoubleTolerance = 0.001;

    public static bool FillStylesAreEqual(FillStyle? fillStyle1, FillStyle? fillStyle2)
    {
        if (fillStyle1 == null || fillStyle2 == null)
        {
            return false;
        }

        return ColorsAreEqual(fillStyle1.Color, fillStyle2.Color)
               && fillStyle1.IsEnabled == fillStyle2.IsEnabled;
    }
    
    public static bool LineStylesAreEqual(LineStyle? lineStyle1, LineStyle? lineStyle2)
    {
        if (lineStyle1 == null || lineStyle2 == null)
        {
            return false;
        }

        return ColorsAreEqual(lineStyle1.Color, lineStyle2.Color)
               && lineStyle1.IsEnabled == lineStyle2.IsEnabled
               && Math.Abs(lineStyle1.Width - lineStyle2.Width) < DoubleTolerance;
    }

    private static bool ColorsAreEqual(RgbaColor c1, RgbaColor c2)
    {
        return Math.Abs(c1.R - c2.R) < DoubleTolerance &&
               Math.Abs(c1.G - c2.G) < DoubleTolerance &&
               Math.Abs(c1.B - c2.B) < DoubleTolerance &&
               Math.Abs(c1.A - c2.A) < DoubleTolerance;
    }
}