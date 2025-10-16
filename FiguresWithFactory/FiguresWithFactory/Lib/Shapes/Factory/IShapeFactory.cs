namespace FiguresWithFactory.Lib.Shapes.Factory;

public interface IShapeFactory
{
    public Shape CreateShape(string description);
}