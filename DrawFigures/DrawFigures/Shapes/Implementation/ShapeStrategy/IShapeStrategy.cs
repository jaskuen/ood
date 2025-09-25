using DrawFigures.Canvas;

namespace DrawFigures.Shapes.Implementation.ShapeStrategy;

public interface IShapeStrategy
{
    public void Draw(ICanvas canvas);
    public void MoveBy(float x, float y);
    public string StringParams();
}