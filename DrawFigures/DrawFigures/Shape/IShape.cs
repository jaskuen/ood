using DrawFigures.Shape.Implementation.ShapeStrategy;

namespace DrawFigures.Shape;

public interface IShape
{
    public void Draw();
    public void Move(float x, float y);
    public void SetShapeStrategy(IShapeStrategy strategy);
    public void SetColor(string color);
    public string ToString();
}