using Adapter.GraphicsLib;

namespace Adapter.ShapeDrawingLib.Implementation;

public class Triangle : ICanvasDrawable
{
    private readonly Point _point1;
    private readonly Point _point2;
    private readonly Point _point3;
    
    public Triangle(Point p1, Point p2, Point p3)
    {
        _point1 = p1;
        _point2 = p2;
        _point3 = p3;
    }
    
    public void Draw(ICanvas canvas)
    {
        canvas.MoveTo(_point1.X, _point1.Y);
        canvas.LineTo(_point2.X, _point2.Y);
        canvas.MoveTo(_point2.X, _point2.Y);
        canvas.LineTo(_point3.X, _point3.Y);
        canvas.MoveTo(_point3.X, _point3.Y);
        canvas.LineTo(_point1.X, _point1.Y);
    }
}