using Slides.Lib.Canvas;
using Slides.Lib.Shapes.Implementation;

namespace Slides.Lib.Shapes;

public interface IShape
{
    public void Draw(ICanvas canvas);
    public Frame GetFrame();
    public void SetFrame(double x1, double y1, double x2, double y2);
    public LineStyle? GetLineStyle();
    public FillStyle? GetFillStyle();
    public void SetLineStyle(bool isEnabled, RgbaColor color, double width = 1);
    public void SetFillStyle(bool isEnabled, RgbaColor color);
    public IShapes? GetShapesGroup();
}