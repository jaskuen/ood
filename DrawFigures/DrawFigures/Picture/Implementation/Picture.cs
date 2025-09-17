using DrawFigures.Canvas;
using DrawFigures.Drawer;
using DrawFigures.Shape;
using DrawFigures.Shape.Implementation;
using DrawFigures.Shape.Implementation.ShapeStrategy;
using DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;
using SkiaSharp;

namespace DrawFigures.Picture.Implementation;

public class Picture : IPicture
{
    private readonly ICanvas _canvas;
    private IDictionary<string, IShape> _shapes;

    public Picture(int width, int height)
    {
        _canvas = new Canvas.Implementation.Canvas(width, height);
        _shapes = new Dictionary<string, IShape>();
    }

    public void DrawFigure(string id)
    {
        IShape shape = GetShape(id);
        shape.Draw();
    }

    public void DrawPicture()
    {
        foreach (IShape shape in _shapes.Values)
        {
            shape.Draw();
        }
    }

    public void AddShape(string id, string color, string shape, List<string> parameters)
    {
        if (_shapes.ContainsKey(id))
        {
            throw new Exception("Shape already exists");
        }

        IShape shapeToAdd = new Shape.Implementation.Shape(color, _canvas);

        shapeToAdd.SetShapeStrategy(ShapeStrategyUtils.CreateShapeStrategy(shape, parameters));

        _shapes.Add(id, shapeToAdd);
    }

    public void DeleteShape(string id)
    {
        if (!_shapes.Remove(id))
        {
            throw new Exception("No shape found with id " + id);
        }
    }

    public IShape GetShape(string id)
    {
        if (!_shapes.ContainsKey(id))
        {
            throw new Exception("No shape found with id " + id);
        }

        return _shapes[id];
    }

    public List<IShape> GetShapes()
    {
        return _shapes.Values.ToList();
    }

    public void MoveShape(string id, float x, float y)
    {
        IShape shape = GetShape(id);
        shape.Move(x, y);
    }

    public void MovePicture(float x, float y)
    {
        foreach (IShape shape in GetShapes())
        {
            shape.Move(x, y);
        }
    }

    public void ChangeShapeColor(string id, string color)
    {
        IShape shape = GetShape(id);
        shape.SetColor(color);
    }

    public void ChangeShapeType(string id, string shape, List<string> parameters)
    {
        IShape shapeToChange = GetShape(id);

        shapeToChange.SetShapeStrategy(ShapeStrategyUtils.CreateShapeStrategy(shape, parameters));
    }

    public void List()
    {
        foreach ( var item in _shapes.Select((value, i) => new { i, value }) )
        {
            Console.WriteLine($"{item.i} {item.value.Key} {item.value.Value.ToString()}");
        }
    }

    public SKBitmap GetBitmap()
    {
        return _canvas.GetBitmap();
    }
}