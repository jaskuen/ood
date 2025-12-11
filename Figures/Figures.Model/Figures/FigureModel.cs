using Figures.Model.Core;
using Figures.Model.Core.Events;

namespace Figures.Model.Figures;

/// <summary>
/// Абстрактный базовый класс для всех геометрических фигур.
/// </summary>
public abstract class FigureModel(string id, FigureKind type, FigureAttributes? attributes = null, int zIndex = 0)
    : EventEmitter
{
    private FigureAttributes _attributes = attributes ?? FigureAttributes.Default;

    public string Id { get; } = id;

    protected FigureKind Type { get; } = type;

    public int ZIndex { get; } = zIndex;

    public FigureAttributes Attributes => _attributes;

    public void SetAttributes(FigureAttributes attrs)
    {
        _attributes = attrs;
        Emit(EventType.AttributesChanged, _attributes);
    }

    public abstract Rect GetBoundingBox();
    public abstract bool ContainsPoint(Point point);
    public abstract void Resize(Rect box);
    public abstract SerializedFigure Serialize();
}

