using Slides.Lib.Canvas;
using Slides.Lib.Extensions;

namespace Slides.Lib.Shapes.Implementation;

public class ShapeGroup : Shape, IShapes
{
    private readonly IList<Shape> _shapes = [];

    private bool _isUpdatingLineStyle = false;
    private bool _isUpdatingFillStyle = false;

    public ShapeGroup(IList<Shape> shapes)
    {
        for (int i = 0; i < shapes.Count; i++)
        {
            InsertShape(shapes[i], i);
        }
    }

    protected override void DoDraw(ICanvas canvas)
    {
        foreach (Shape shape in _shapes)
        {
            shape.Draw(canvas);
        }
    }

    public override Frame GetFrame()
    {
        Point leftTop = new Point(double.MaxValue, double.MaxValue);
        Point rightBottom = new Point(double.MinValue, double.MinValue);

        foreach (Shape shape in _shapes)
        {
            Frame frame = shape.GetFrame();

            if (frame.LeftTop.X <= leftTop.X && frame.LeftTop.Y <= leftTop.Y)
            {
                leftTop.X = frame.LeftTop.X;
                leftTop.Y = frame.LeftTop.Y;
            }

            if (frame.RightBottom.X >= rightBottom.X && frame.RightBottom.Y >= rightBottom.Y)
            {
                rightBottom.X = frame.RightBottom.X;
                rightBottom.Y = frame.RightBottom.Y;
            }
        }

        return new Frame(leftTop.X, leftTop.Y, rightBottom.X, rightBottom.Y);
    }

    public override void SetFrame(double x1, double y1, double x2, double y2)
    {
        if (_shapes.Count == 0)
        {
            return;
        }

        var groupFrame = GetFrame();
        double groupWidth = groupFrame.RightBottom.X - groupFrame.LeftTop.X;
        double groupHeight = groupFrame.RightBottom.Y - groupFrame.LeftTop.Y;

        if (groupWidth == 0 || groupHeight == 0)
        {
            return;
        }

        double newWidth = x2 - x1;
        double newHeight = y2 - y1;
        double scaleWidth = newWidth / groupWidth;
        double scaleHeight = newHeight / groupHeight;

        foreach (Shape shape in _shapes)
        {
            var shapeFrame = shape.GetFrame();
            double shapeLeftInGroupFrame = shapeFrame.LeftTop.X - groupFrame.LeftTop.X;
            double shapeTopInGroupFrame = shapeFrame.LeftTop.Y - groupFrame.LeftTop.Y;
            double shapeWidth = shapeFrame.RightBottom.X - shapeFrame.LeftTop.X;
            double shapeHeight = shapeFrame.RightBottom.Y - shapeFrame.LeftTop.Y;

            double newX1 = x1 + shapeLeftInGroupFrame * scaleWidth;
            double newY1 = y1 + shapeTopInGroupFrame * scaleHeight;
            double newX2 = newX1 + shapeWidth * scaleWidth;
            double newY2 = newY1 + shapeHeight * scaleHeight;

            shape.SetFrame(newX1, newY1, newX2, newY2);
        }
    }

    public override LineStyle? GetLineStyle()
    {
        return LineStyle;
    }

    public override FillStyle? GetFillStyle()
    {
        return FillStyle;
    }

    protected override void DoSetLineStyleToChildren(bool isEnabled, RgbaColor color, double width = 1)
    {
        if (!_isUpdatingLineStyle)
        {
            _isUpdatingLineStyle = true;
            foreach (Shape shape in _shapes)
            {
                shape.SetLineStyle(isEnabled, color, width);
            }

            _isUpdatingLineStyle = false;
        }
    }

    protected override void DoSetFillStyleToChildren(bool isEnabled, RgbaColor color)
    {
        if (!_isUpdatingFillStyle)
        {
            _isUpdatingFillStyle = true;
            foreach (Shape shape in _shapes)
            {
                shape.SetFillStyle(isEnabled, color);
            }

            _isUpdatingFillStyle = false;
        }
    }

    public override IShapes? GetShapesGroup()
    {
        return this;
    }

    public int GetShapesCount() => _shapes.Count;

    public void InsertShape(Shape shape, int position)
    {
        if (GetParents().Contains(shape))
        {
            throw new Exception("Trying to add some-level parent shape as a child");
        }

        shape.SetParent(this);

        if (position >= _shapes.Count)
        {
            _shapes.Add(shape);
        }
        else
        {
            _shapes.Insert(position, shape);
        }
    }

    public Shape GetShapeAt(int position)
    {
        if (position >= _shapes.Count)
        {
            throw new IndexOutOfRangeException("Get shape position out of range");
        }

        return _shapes[position];
    }

    public void RemoveShapeAt(int position)
    {
        if (position >= _shapes.Count)
        {
            throw new IndexOutOfRangeException("Remove shape position out of range");
        }

        _shapes.RemoveAt(position);
    }

    public void Dispose()
    {
        for (int i = 0; i < _shapes.Count; i++)
        {
            Shape shape = GetShapeAt(i);

            IShapes? group = shape.GetShapesGroup();
            group?.Dispose();
        }
    }

    internal void UpdateStrokeStyle()
    {
        List<LineStyle?> lineStyles = _shapes.Select(x => x.GetLineStyle()).ToList();
        LineStyle? someLineStyle = lineStyles.FirstOrDefault();

        if (lineStyles.TrueForAll(x =>
                ComparisonExtensions.LineStylesAreEqual(x, someLineStyle)))
        {
            if (someLineStyle != null)
            {
                SetLineStyle(someLineStyle.IsEnabled, someLineStyle.Color, someLineStyle.Width);
            }

            return;
        }

        LineStyle = null;
        Parent?.UpdateStrokeStyle();
    }

    internal void UpdateFillStyle()
    {
        List<FillStyle?> fillStyles = _shapes.Select(x => x.GetFillStyle()).ToList();
        FillStyle? someFillStyle = fillStyles.FirstOrDefault();

        if (fillStyles.TrueForAll(x =>
                ComparisonExtensions.FillStylesAreEqual(x, someFillStyle)))
        {
            if (someFillStyle != null)
            {
                SetLineStyle(someFillStyle.IsEnabled, someFillStyle.Color);
            }

            return;
        }

        FillStyle = null;
        Parent?.UpdateFillStyle();
    }
}