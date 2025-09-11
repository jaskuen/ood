using DrawFigures.Canvas;
using DrawFigures.Shape.Implementation.ShapeStrategy;
using SkiaSharp;

namespace DrawFigures.Shape.Implementation;

public class Shape : IShape
{
    private readonly ICanvas _canvas;
    private IShapeStrategy _shapeStrategy;
    public string Id { get; init; }
    public string Color { get; set; }

    public Shape(string id, string color, ICanvas canvas, IShapeStrategy strategy)
    {
        Id = id;
        _canvas = canvas;
        _shapeStrategy = strategy;
        Color = color;
    }

    public void Draw()
    {
        _shapeStrategy.Draw(_canvas);
    }

    public void Move(double x, double y)
    {
        _shapeStrategy.MoveTo(x, y);
    }

    public void SetShapeStrategy(IShapeStrategy strategy)
    {
        _shapeStrategy = strategy;
    }
}