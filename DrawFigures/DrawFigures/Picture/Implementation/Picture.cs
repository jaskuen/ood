using DrawFigures.Canvas;
using DrawFigures.Shape;

namespace DrawFigures.Picture.Implementation;

public class Picture : IPicture
{
    private readonly ICanvas _canvas;
    private IList<IShape> _shapes;

    public Picture(ICanvas canvas)
    {
        _canvas = canvas;
        _shapes = new List<IShape>();
    }

    public void DrawFigure(string id)
    {
        throw new NotImplementedException();
    }

    public void DrawPicture()
    {
        throw new NotImplementedException();
    }

    public void AddShape(string id, string color, string shape, string parameters)
    {
        throw new NotImplementedException();
    }

    public void DeleteShape(string id)
    {
        throw new NotImplementedException();
    }

    public void List()
    {
        throw new NotImplementedException();
    }
}