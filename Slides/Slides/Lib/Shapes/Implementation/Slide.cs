using Slides.Lib.Canvas;

namespace Slides.Lib.Shapes.Implementation;

public class Slide : ISlide
{
    private readonly double _width;
    private readonly double _height;
    private RgbaColor _backgroundColor;
    private readonly List<Shape> _shapes;

    public Slide(double width = 800, double height = 600, RgbaColor? backgroundColor = null)
    {
        _width = width;
        _height = height;
        _backgroundColor = backgroundColor ?? new RgbaColor(255, 255, 255, 255); // Default: white, opaque
        _shapes = new List<Shape>();
    }

    public double GetWidth()
    {
        return _width;
    }

    public double GetHeight()
    {
        return _height;
    }

    public int GetShapesCount()
    {
        return _shapes.Count;
    }

    public Shape GetShapeAtIndex(int index)
    {
        if (index < 0 || index >= _shapes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
        }

        return _shapes[index];
    }

    public void InsertShape(Shape shape, int position = int.MaxValue)
    {
        if (shape == null)
        {
            throw new ArgumentNullException(nameof(shape), "Shape cannot be null.");
        }

        if (position < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "Position cannot be negative.");
        }

        if (position >= _shapes.Count)
        {
            _shapes.Add(shape);
        }
        else
        {
            _shapes.Insert(position, shape);
        }
    }

    public void InsertShapeGroup(IList<int> indexes)
    {
        if (indexes == null)
        {
            throw new ArgumentNullException(nameof(indexes), "Indexes cannot be null.");
        }

        if (!indexes.Any() || indexes.Any(i => i < 0 || i >= _shapes.Count))
        {
            throw new ArgumentException("Invalid or empty index set.", nameof(indexes));
        }

        List<int> sortedIndexes = indexes.OrderByDescending(i => i).ToList();
        List<Shape> shapesToGroup = [];

        foreach (int index in sortedIndexes)
        {
            shapesToGroup.Add(_shapes[index]);
        }

        Shape group = new ShapeGroup(shapesToGroup);

        foreach (var index in sortedIndexes)
        {
            _shapes.RemoveAt(index);
        }

        int insertPosition = sortedIndexes.Last();
        InsertShape(group, insertPosition);
    }

    public void RemoveShapeAtIndex(int index)
    {
        if (index < 0 || index >= _shapes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
        }

        _shapes.RemoveAt(index);
    }

    public RgbaColor GetBackgroundColor()
    {
        return _backgroundColor;
    }

    public void SetBackgroundColor(RgbaColor color)
    {
        _backgroundColor = color;
    }

    public void Draw(ICanvas canvas)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas), "Canvas cannot be null.");
        }

        // Background
        canvas.SetFillColor(_backgroundColor.R, _backgroundColor.G, _backgroundColor.B, _backgroundColor.A);
        canvas.FillPolygon([new Point(0, 0), new Point(_width, 0), new Point(_width, _height), new Point(0, _height)]);

        // Shapes
        foreach (Shape shape in _shapes)
        {
            shape.Draw(canvas);
        }
    }
}