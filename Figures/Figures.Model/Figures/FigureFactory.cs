using System.Text.Json;
using Figures.Model.Core;

namespace Figures.Model.Figures;

/// <summary>
/// Статическая фабрика для создания экземпляров FigureModel.
/// </summary>
public static class FigureFactory
{
    public static RectangleModel CreateRectangle(Rect box, FigureAttributes? attrs = null)
    {
        return new RectangleModel(box, attrs);
    }

    public static EllipseModel CreateEllipse(Rect box, FigureAttributes? attrs = null)
    {
        return new EllipseModel(box, attrs);
    }

    public static TriangleModel CreateTriangle(Rect box,
        FigureAttributes? attrs = null)
    {
        return new TriangleModel(box, attrs);
    }

    public static ImageModel CreateImage(Rect box, string imageId, byte[]? imageData,
        FigureAttributes? attrs = null)
    {
        return new ImageModel(box, imageId, imageData, attrs);
    }

    public static FigureModel FromSerialized(SerializedFigure serialized, Func<string, byte[]?> resolveImage)
    {
        float GetFloat(string key)
        {
            if (!serialized.Data.TryGetValue(key, out var value)) return 0;
            if (value is JsonElement jsonElement)
            {
                return jsonElement.GetSingle();
            }

            return Convert.ToSingle(value);
        }

        string? GetString(string key)
        {
            if (!serialized.Data.TryGetValue(key, out var value)) return null;
            if (value is JsonElement jsonElement)
            {
                return jsonElement.GetString();
            }

            return value.ToString();
        }

        var box = new Rect(
            GetFloat("x"),
            GetFloat("y"),
            GetFloat("width"),
            GetFloat("height")
        );

        return serialized.Type switch
        {
            FigureKind.Rectangle => new RectangleModel(box, serialized.Attributes, serialized.Id),
            FigureKind.Ellipse => new EllipseModel(box, serialized.Attributes, serialized.Id),
            FigureKind.Triangle => new TriangleModel(
                box,
                serialized.Attributes,
                serialized.Id
            ),
            FigureKind.Image => new ImageModel(
                box,
                GetString("imageId") ?? "",
                resolveImage(GetString("imageId") ?? ""),
                serialized.Attributes,
                serialized.Id
            ),
            _ => throw new ArgumentException($"Unsupported figure type: {serialized.Type}")
        };
    }
}