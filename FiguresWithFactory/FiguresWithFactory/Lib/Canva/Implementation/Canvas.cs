using FiguresWithFactory.Lib.Shapes.Factory;

namespace FiguresWithFactory.Lib.Canva.Implementation;

public class Canvas : ICanvas
{
    private string _color;
    private string _drawing;

    public Canvas(int width, int height, Color color = Color.Black)
    {
        _drawing = $"<svg width=\"{width}px\" height=\"{height}px\">";
        _color = color.ParseToString();
    }

    public void SetColor(Color color)
    {
        _color = color.ParseToString();
    }

    public void DrawLine(double x1, double y1, double x2, double y2)
    {
        _drawing += $"<line x1=\"{x1}\" y1=\"{y1}\" x2=\"{x2}\" y2=\"{y2}\" stroke=\"{_color}\" stroke-width=\"3\" />";
    }

    public void DrawEllipse(double cx, double cy, double rx, double ry)
    {
        _drawing += $"<ellipse cx=\"{cx}\" cy=\"{cy}\" rx=\"{rx}\" ry=\"{ry}\" fill=\"none\" stroke=\"{_color}\" stroke-width=\"3\" />";
    }

    public string GetDrawing() => $"{_drawing}</svg>";
}