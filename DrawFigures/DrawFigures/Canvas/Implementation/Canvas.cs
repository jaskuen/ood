using DrawFigures.Shapes.Implementation.ShapeStrategy.Implementation;
using SkiaSharp;

namespace DrawFigures.Canvas.Implementation;

public class Canvas : ICanvas
{
    private SKBitmap _bitmap;
    private SKCanvas _canvas;
    private SKPaint _paint;
    private Point _currentLocation;

    public Canvas(int width, int height)
    {
        _bitmap = new SKBitmap(width, height);
        _canvas = new SKCanvas(_bitmap);
        _canvas.Clear(SKColors.White);
        _paint = new SKPaint
        {
            Color = SKColors.Black,
            StrokeWidth = 2,
            Style = SKPaintStyle.Fill
        };

        _currentLocation = new Point(0, 0);
    }

    public void MoveTo(float x, float y)
    {
        _currentLocation.X = x;
        _currentLocation.Y = y;
    }

    public void SetColor(SKColor color)
    {
        _paint.Color = color;
    }

    public void LineTo(float x, float y)
    {
        _canvas.DrawLine(_currentLocation.X, _currentLocation.Y, x, y, _paint);
        MoveTo(x, y);
    }

    public void DrawEllipse(float x, float y, float rx, float ry)
    {
        var ellipseRect = new SKRect(x - rx, y - ry, x + rx, y + ry);
        _canvas.DrawArc(ellipseRect, 0, 360, true, _paint);
    }

    public void DrawRect(float x, float y, float width, float height)
    {
        var rect = new SKRect(x, y, x + width, y + height);
        _canvas.DrawRect(rect, _paint);
    }

    public void DrawText(float x, float y, float fontSize, string text)
    {
        var font = new SKFont
        {
            Size = fontSize
        };
        _canvas.DrawText(text, x, y, font, _paint);
    }

    public SKBitmap GetBitmap()
    {
        return _bitmap;
    }
}