using Slides.Lib.Shapes.Implementation;

namespace Slides.Lib.Shapes;

public interface IShapes : IDisposable
{
    public int GetShapesCount();
    public void InsertShape(Shape shape, int position);
    public Shape GetShapeAt(int position);
    public void RemoveShapeAt(int position);
}