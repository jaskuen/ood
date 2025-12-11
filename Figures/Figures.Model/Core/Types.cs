namespace Figures.Model.Core;

/// <summary>
/// Представляет точку в 2D пространстве.
/// </summary>
public struct Point(float x, float y)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
}

/// <summary>
/// Представляет размеры объекта (ширина и высота).
/// </summary>
public struct Size(float width, float height)
{
    public float Width { get; set; } = width;
    public float Height { get; set; } = height;
}

/// <summary>
/// Представляет прямоугольную область, комбинируя Point и Size.
/// </summary>
public struct Rect(float x, float y, float width, float height)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
    public float Width { get; set; } = width;
    public float Height { get; set; } = height;
}

/// <summary>
/// Типы поддерживаемых фигур.
/// </summary>
public enum FigureKind
{
    Rectangle,
    Ellipse,
    Triangle,
    Image
}

/// <summary>
/// Атрибуты стиля для фигур.
/// </summary>
public struct FigureAttributes
{
    public uint Fill { get; set; } // ARGB color
    public uint Stroke { get; set; } // ARGB color
    public float StrokeWidth { get; set; }
    public float FillOpacity { get; set; }
    public float StrokeOpacity { get; set; }

    public static FigureAttributes Default => new FigureAttributes
    {
        Fill = 0xFF4A90E2, // Blue
        Stroke = 0xFF000000, // Black
        StrokeWidth = 2.0f,
        FillOpacity = 1.0f,
        StrokeOpacity = 1.0f
    };
}

/// <summary>
/// Типы маркеров изменения размера.
/// </summary>
public enum ResizeHandle
{
    N, NE, E, SE, S, SW, W, NW
}

/// <summary>
/// Интерфейс для сериализованного представления фигуры.
/// </summary>
public class SerializedFigure
{
    public string Id { get; set; } = string.Empty;
    public FigureKind Type { get; set; }
    public int ZIndex { get; set; }
    public Dictionary<string, object> Data { get; set; } = new();
    public FigureAttributes Attributes { get; set; }
}

/// <summary>
/// Интерфейс для сериализованного представления всего документа.
/// </summary>
public class DocumentData
{
    public string FileName { get; set; } = "Без названия";
    public CanvasData Canvas { get; set; } = new();
    public List<SerializedFigure> Figures { get; set; } = new();
    public Dictionary<string, ImageData>? Images { get; set; }
}

public class CanvasData
{
    public float Width { get; set; }
    public float Height { get; set; }
}

public class ImageData
{
    public string DataUrl { get; set; } = string.Empty;
    public string? Name { get; set; }
}

