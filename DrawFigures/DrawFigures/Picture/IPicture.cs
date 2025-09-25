using DrawFigures.Canvas;
using DrawFigures.Shapes.Implementation;
using DrawFigures.Shapes.Implementation.ShapeStrategy;
using SkiaSharp;

namespace DrawFigures.Picture;

public interface IPicture
{
    public void DrawFigure(string id);
    public void DrawPicture();
    public void AddShape(string id, Shape shape);
    public void DeleteShape(string id);
    public Shape GetShape(string id);
    public List<Shape> GetShapes();
    public void MoveShape(string id, float x, float y);
    public void MovePicture(float x, float y);
    public void ChangeShapeColor(string id, string color);
    public void ChangeShapeType(string id, IShapeStrategy newShapeStrategy);

    public void List();

    // не привязываться к конкретной реализации рисования
    // не привязывать bitmap к picture
    public ICanvas GetCanvas();
}