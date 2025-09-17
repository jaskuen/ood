using SkiaSharp;
namespace DrawFigures.Canvas;

public interface ICanvas
{
    public void MoveTo(float x, float y);
    public void SetColor(SKColor color);
    public void LineTo(float x, float y);
    public void DrawEllipse(float x, float y, float r);
    public void DrawRect(float x, float y, float width, float height);
    public void DrawText(float x, float y, float fontSize, string text);
    public SKBitmap GetBitmap();
}