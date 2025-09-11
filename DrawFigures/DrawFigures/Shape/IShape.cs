using DrawFigures.Shape.Implementation.ShapeStrategy;

namespace DrawFigures.Shape;

public interface IShape
{
    public void Draw();
    public void Move(double x, double y);
    public void SetShapeStrategy(IShapeStrategy strategy);
}