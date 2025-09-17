using DrawFigures.Shape;
using DrawFigures.Shape.Implementation;
using SkiaSharp;

namespace DrawFigures.Picture;

public interface IPicture
{
    public void DrawFigure(string id);
    public void DrawPicture();
    public void AddShape(string id, string color, string shape, List<string> parameters);
    public void DeleteShape(string id);
    public IShape GetShape(string id);
    public List<IShape> GetShapes();
    public void MoveShape(string id, float x, float y);
    public void MovePicture(float x, float y);
    public void ChangeShapeColor(string id, string color);
    public void ChangeShapeType(string id, string shape, List<string> parameters);
    public void List();
    public SKBitmap GetBitmap();
}