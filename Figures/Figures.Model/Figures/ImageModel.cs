using Figures.Model.Core;
using Figures.Model.Core.Events;

namespace Figures.Model.Figures;

/// <summary>
/// Представляет модель фигуры-изображения.
/// </summary>
public class ImageModel(
    Rect box,
    string imageId,
    byte[]? imageData,
    FigureAttributes? attributes = null,
    string? id = null)
    : FigureModel(id ?? Guid.NewGuid().ToString(), FigureKind.Image, attributes)
{
    private Rect _box = box;
    private string _imageId = imageId;
    private byte[]? _imageData = imageData;

    public string ImageId => _imageId;
    public byte[]? ImageData => _imageData;

    public void SetImageSource(string imageId, byte[]? imageData)
    {
        _imageId = imageId;
        _imageData = imageData;
        Emit(EventType.ImageChanged, new { ImageId = imageId, ImageData = imageData });
    }

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
        _box = new Rect(box.X, box.Y, Math.Max(10, box.Width), Math.Max(10, box.Height));
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
                { "height", box.Height },
                { "imageId", _imageId }
            }
        };
    }
}

