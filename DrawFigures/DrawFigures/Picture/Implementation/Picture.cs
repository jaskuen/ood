using DrawFigures.Canvas;
using DrawFigures.Shapes.Implementation;
using DrawFigures.Shapes.Implementation.ShapeStrategy;

namespace DrawFigures.Picture.Implementation;

// убрать методы, адресованные напрямую к фигуре
public class Picture : IPicture
{
    private readonly ICanvas _canvas;
    private IDictionary<string, Shape> _shapes;

    public Picture(int width, int height)
    {
        _canvas = new Canvas.Implementation.Canvas(width, height);
        _shapes = new Dictionary<string, Shape>();
    }

    public void DrawFigure(string id)
    {
        var shape = GetShape(id);
        shape.Draw();
    }

    public void DrawPicture()
    {
        foreach (var shape in _shapes.Values) shape.Draw();
    }

    public void AddShape(string id, Shape shapeToAdd)
        // не передавать параметры
    {
        if (_shapes.ContainsKey(id)) throw new Exception("Shape already exists");

        _shapes.Add(id, shapeToAdd);
    }

    public void DeleteShape(string id)
    {
        if (!_shapes.Remove(id)) throw new Exception("No shape found with id " + id);
    }

    public Shape GetShape(string id)
    {
        if (!_shapes.ContainsKey(id)) throw new Exception("No shape found with id " + id);

        return _shapes[id];
    }

    public List<Shape> GetShapes()
    {
        return _shapes.Values.ToList();
    }

    // убрать
    public void MoveShape(string id, float x, float y)
    {
        var shape = GetShape(id);
        shape.Move(x, y);
    }

    public void MovePicture(float x, float y)
    {
        foreach (var shape in GetShapes()) shape.Move(x, y);
    }

    public void ChangeShapeColor(string id, string color)
    {
        var shape = GetShape(id);
        shape.SetColor(color);
    }

    public void ChangeShapeType(string id, IShapeStrategy newStrategy)
    {
        var shapeToChange = GetShape(id);

        shapeToChange.SetShapeStrategy(newStrategy);
    }

    public void List()
    {
        foreach (var item in _shapes.Select((value, i) => new { i, value }))
            Console.WriteLine($"{item.i + 1} {item.value.Key} {item.value.Value.ToString()}");
    }

    public ICanvas GetCanvas()
    {
        return _canvas;
    }
}