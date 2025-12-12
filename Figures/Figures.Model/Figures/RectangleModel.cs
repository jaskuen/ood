using Figures.Model.Core;
using Figures.Model.Core.Events;

namespace Figures.Model.Figures;

/// <summary>
/// Прямоугольник
/// </summary>
public class RectangleModel(Rect box, FigureAttributes? attributes = null, string? id = null)
    : FigureModel(id ?? Guid.NewGuid().ToString(), FigureKind.Rectangle, attributes)
{
    private Rect _box = box;

    public override Rect GetBoundingBox() => _box;

    public override bool ContainsPoint(Point point)
    {
        return point.X >= _box.X &&
               point.X <= _box.X + _box.Width &&
               point.Y >= _box.Y &&
               point.Y <= _box.Y + _box.Height;
    }

    public override void Resize(Rect box)
    {
        _box = new Rect(box.X, box.Y, Math.Max(1, box.Width), Math.Max(1, box.Height));
        Emit(EventType.GeometryChanged, _box);
    }

    public override SerializedFigure Serialize()
    {
        var box = GetBoundingBox();
        return new SerializedFigure
        {
            Id = Id,
            Type = Type,
            ZIndex = ZIndex,
            Attributes = Attributes,
            Data = new Dictionary<string, object>
            {
                { "x", box.X },
                { "y", box.Y },
                { "width", box.Width },
                { "height", box.Height }
            }
        };
    }
}

