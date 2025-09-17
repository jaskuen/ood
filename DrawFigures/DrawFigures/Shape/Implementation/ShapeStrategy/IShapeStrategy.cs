using DrawFigures.Canvas;

namespace DrawFigures.Shape.Implementation.ShapeStrategy;

public interface IShapeStrategy
{
    public void Draw(ICanvas canvas);
    public void MoveBy(float x, float y);
    public string StringParams();
}