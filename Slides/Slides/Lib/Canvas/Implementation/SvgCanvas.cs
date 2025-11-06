namespace Slides.Lib.Canvas.Implementation;

public class SvgCanvas : ICanvas
{
    private RgbaColor _fillColor;
    private RgbaColor _strokeColor;
    private double _strokeWidth;

    private readonly TextWriter _writer;

    public SvgCanvas(TextWriter writer)
    {
        _writer = writer;
        _writer.WriteLine("<svg xmlns=\"http://www.w3.org/2000/svg\">");

        _fillColor = new RgbaColor(0, 0, 0, 1);
        _strokeColor = new RgbaColor(0, 0, 0, 1);
        _strokeWidth = 1.0;
    }

    public void DrawLine(Point p1, Point p2)
    {
        string stroke = FormatColor(_strokeColor);
        _writer.WriteLine($"<line x1=\"{p1.X}\" y1=\"{p1.Y}\" x2=\"{p2.X}\" y2=\"{p2.Y}\" " +
                          $"stroke=\"{stroke}\" stroke-width=\"{_strokeWidth}\" />");
    }

    public void DrawEllipse(Point center, double rx, double ry)
    {
        string stroke = FormatColor(_strokeColor);
        _writer.WriteLine($"<ellipse cx=\"{center.X}\" cy=\"{center.Y}\" rx=\"{rx}\" ry=\"{ry}\" " +
                          $"stroke=\"{stroke}\" stroke-width=\"{_strokeWidth}\" fill=\"none\" />");
    }

    public void FillEllipse(Point center, double rx, double ry)
    {
        string fill = FormatColor(_fillColor);
        _writer.WriteLine($"<ellipse cx=\"{center.X}\" cy=\"{center.Y}\" rx=\"{rx}\" ry=\"{ry}\" " +
                          $"fill=\"{fill}\" />");
    }

    public void FillPolygon(Point[] points)
    {
        if (points.Length < 2)
        {
            return;
        }

        string fill = FormatColor(_fillColor);
        string pointsStr = string.Join(" ", points.Select(p => $"{p.X},{p.Y}"));
        _writer.WriteLine($"<polygon points=\"{pointsStr}\" fill=\"{fill}\" />");
    }

    public void SetFillColor(double r, double g, double b, double a)
    {
        _fillColor = new RgbaColor
        (
            Clamp(r),
            Clamp(g),
            Clamp(b),
            Clamp(a)
        );
    }

    public void SetStrokeColor(double r, double g, double b, double a)
    {
        _strokeColor = new RgbaColor
        (
            Clamp(r),
            Clamp(g),
            Clamp(b),
            Clamp(a)
        );
    }

    public void SetStrokeWidth(double w)
    {
        _strokeWidth = w >= 0 ? w : 0;
    }

    private string FormatColor(RgbaColor color)
    {
        int r = (int)(color.R * 255);
        int g = (int)(color.G * 255);
        int b = (int)(color.B * 255);
        double a = color.A;
        return $"rgba({r}, {g}, {b}, {a})";
    }

    private double Clamp(double value)
    {
        return Math.Max(0, Math.Min(1, value));
    }
}