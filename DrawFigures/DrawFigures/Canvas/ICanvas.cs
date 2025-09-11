using SkiaSharp;
namespace DrawFigures.Canvas;

public interface ICanvas
{
    public void MoveTo(double x, double y);
    public void SetColor(SKColor color);
    public void LineTo(double x, double y);
    public void DrawEllipse(double x, double y, double rx, double ry);
    public void DrawText(double x, double y, double fontSize, string text);
}