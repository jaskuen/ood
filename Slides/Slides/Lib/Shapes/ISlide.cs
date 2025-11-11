using Slides.Lib.Canvas;
using Slides.Lib.Shapes.Implementation;

namespace Slides.Lib.Shapes;

public interface ISlide
{
    public double GetWidth();
    public double GetHeight();
    public int GetShapesCount();
    public Shape GetShapeAtIndex(int index);
    public void InsertShape(Shape shape, int position = int.MaxValue);
    public void InsertShapeGroup(IList<int> indexes);
    public void RemoveShapeAtIndex(int index);
    public RgbaColor GetBackgroundColor();
    public void SetBackgroundColor(RgbaColor color);
    public void Draw(ICanvas canvas);
}