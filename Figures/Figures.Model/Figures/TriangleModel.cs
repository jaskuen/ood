using Figures.Model.Core;
using Figures.Model.Core.Events;
using Figures.Model.Core.Utils;

namespace Figures.Model.Figures;

/// <summary>
/// Представляет модель треугольной фигуры.
/// </summary>
public class TriangleModel(
    Rect box,
    FigureAttributes? attributes = null,
    string? id = null)
    : FigureModel(id ?? Guid.NewGuid().ToString(), FigureKind.Triangle, attributes)
{
    private Rect _box = box;

    public override Rect GetBoundingBox() => _box;

    private (Point, Point, Point) GetPoints()
    {
        return (
            new Point(_box.X + _box.Width / 2, _box.Y),
            new Point(_box.X + _box.Width, _box.Y + _box.Height),
            new Point(_box.X, _box.Y + _box.Height)
        );
    }

    public override bool ContainsPoint(Point point)
    {
        var (a, b, c) = GetPoints();
        return Geometry.PointInTriangle(point, a, b, c);
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