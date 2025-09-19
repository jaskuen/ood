using DrawFigures.Canvas;
using DrawFigures.Picture.Implementation;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public class OvalStrategy : IShapeStrategy
{
    public OvalStrategy(string cx, string cy, string rx, string ry)
    {
        _center = new Point(cx.ToFloat(), cy.ToFloat());
        _radiusX = rx.ToFloat();
        _radiusY = ry.ToFloat();
    }
    
    private readonly Point _center;
    private readonly float _radiusX;
    private readonly float _radiusY;
    
    public void Draw(ICanvas canvas)
    {
        canvas.DrawEllipse(_center.X, _center.Y, _radiusX, _radiusY);
    }

    public void MoveBy(float x, float y)
    {
        _center.X += x;
        _center.Y += y;
    }

    public string StringParams() => $"{_center.X} {_center.Y} {_radiusX} {_radiusY}";
}