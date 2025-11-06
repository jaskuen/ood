using Adapter.GraphicsLib;

namespace Adapter.ShapeDrawingLib.Implementation;

public class Rectangle : ICanvasDrawable
{
    private readonly Point _leftTop;
    private readonly int _width;
    private readonly int _height;
    private readonly int _color;

    public Rectangle(Point leftTop, int width, int height, int color = 0x000000)
    {
        _leftTop = leftTop;
        _width = width;
        _height = height;
        _color = color;
    }
    
    public void Draw(ICanvas canvas)
    {
        canvas.SetColor(_color);
        canvas.MoveTo(_leftTop.X, _leftTop.Y);
        canvas.LineTo(_leftTop.X + _width,  _leftTop.Y);
        canvas.MoveTo(_leftTop.X + _width,  _leftTop.Y);
        canvas.LineTo(_leftTop.X + _width,  _leftTop.Y + _height);
        canvas.MoveTo(_leftTop.X + _width,  _leftTop.Y + _height);
        canvas.LineTo(_leftTop.X,  _leftTop.Y + _height);
        canvas.MoveTo(_leftTop.X,  _leftTop.Y + _height);
        canvas.LineTo(_leftTop.X,  _leftTop.Y);
    }
}