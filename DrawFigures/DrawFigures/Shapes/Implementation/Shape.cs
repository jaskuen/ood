using DrawFigures.Canvas;
using DrawFigures.Shapes.Implementation.ShapeStrategy;
using SkiaSharp;

namespace DrawFigures.Shapes.Implementation;

public class Shape
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

    public override string ToString()
    {
        return $"{_color} {_shapeStrategy.StringParams()}";
    }
}