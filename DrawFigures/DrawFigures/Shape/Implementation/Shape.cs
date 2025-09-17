using DrawFigures.Canvas;
using DrawFigures.Shape.Implementation.ShapeStrategy;
using SkiaSharp;

namespace DrawFigures.Shape.Implementation;

public class Shape : IShape
{
    private readonly ICanvas _canvas;
    private IShapeStrategy _shapeStrategy;
    private string _color;

    public Shape(string color, ICanvas canvas)
    {
        _canvas = canvas;
        _color = color;
    }

    public void Draw()
    {
        _canvas.SetColor(SKColor.Parse(_color));
        _shapeStrategy.Draw(_canvas);
    }

    public void Move(float x, float y)
    {
        _shapeStrategy.MoveBy(x, y);
    }

    public void SetShapeStrategy(IShapeStrategy strategy)
    {
        _shapeStrategy = strategy;
    }

    public void SetColor(string color)
    {
        _color = color;
    }

    public override string ToString() => $"{_color} {_shapeStrategy.StringParams()}";
}