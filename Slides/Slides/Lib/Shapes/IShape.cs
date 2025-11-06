using Slides.Lib.Canvas;

namespace Slides.Lib.Shapes;

public interface IShape
{
    public void Draw(ICanvas canvas);
    public Frame GetFrame();
    public LineStyle GetLineStyle();
    public FillStyle GetFillStyle();
}