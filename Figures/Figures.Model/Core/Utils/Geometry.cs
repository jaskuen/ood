namespace Figures.Model.Core.Utils;

/// <summary>
/// Утилиты для геометрических вычислений.
/// </summary>
public static class Geometry
{
    /// <summary>
    /// Ограничивает числовое значение заданным диапазоном.
    /// </summary>
    public static float Clamp(float value, float min, float max)
    {
        return Math.Min(Math.Max(value, min), max);
    }

    /// <summary>
    /// Ограничивает прямоугольник пределами холста.
    /// </summary>
    public static Rect LimitRectToCanvas(Rect rect, Size canvas)
    {
        var width = Math.Min(rect.Width, canvas.Width);
        var height = Math.Min(rect.Height, canvas.Height);

        var x = Clamp(rect.X, 0, canvas.Width - width);
        var y = Clamp(rect.Y, 0, canvas.Height - height);

        return new Rect(x, y, width, height);
    }

    /// <summary>
    /// Проверяет, находится ли точка внутри треугольника (барицентрические координаты).
    /// </summary>
    public static bool PointInTriangle(Point point, Point a, Point b, Point c)
    {
        var area = 0.5f * Math.Abs((b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y));
        if (area < 0.0001f) return false;

        var v0x = c.X - a.X;
        var v0y = c.Y - a.Y;
        var v1x = b.X - a.X;
        var v1y = b.Y - a.Y;
        var v2x = point.X - a.X;
        var v2y = point.Y - a.Y;

        var dot00 = v0x * v0x + v0y * v0y;
        var dot01 = v0x * v1x + v0y * v1y;
        var dot02 = v0x * v2x + v0y * v2y;
        var dot11 = v1x * v1x + v1y * v1y;
        var dot12 = v1x * v2x + v1y * v2y;

        var invDenom = 1.0f / (dot00 * dot11 - dot01 * dot01);
        var u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        var v = (dot00 * dot12 - dot01 * dot02) * invDenom;

        return u >= 0 && v >= 0 && u + v <= 1;
    }

    /// <summary>
    /// Проверяет, находится ли точка внутри эллипса.
    /// </summary>
    public static bool PointInEllipse(Point point, Rect rect)
    {
        var rx = rect.Width / 2;
        var ry = rect.Height / 2;
        var cx = rect.X + rx;
        var cy = rect.Y + ry;
        var value = Math.Pow((point.X - cx) / rx, 2) + Math.Pow((point.Y - cy) / ry, 2);
        return value <= 1;
    }
}

