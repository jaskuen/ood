using Figures.Model.Core;
using Figures.Model.Core.Events;
using Figures.Model.Core.Utils;

namespace Figures.Model.Figures;

/// <summary>
/// Представляет модель эллиптической фигуры.
/// </summary>
public class EllipseModel(Rect box, FigureAttributes? attributes = null, string? id = null)
    : FigureModel(id ?? Guid.NewGuid().ToString(), FigureKind.Ellipse, attributes)
{
    private Rect _box = box;

    public override Rect GetBoundingBox() => _box;

    public override bool ContainsPoint(Point point)
    {
        return Geometry.PointInEllipse(point, _box);
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

