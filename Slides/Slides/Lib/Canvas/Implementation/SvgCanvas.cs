using System.Globalization;

namespace Slides.Lib.Canvas.Implementation;

public class SvgCanvas : ICanvas, IDisposable
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
        _writer.WriteLine($"<line x1=\"{p1.X.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"y1=\"{p1.Y.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"x2=\"{p2.X.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"y2=\"{p2.Y.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"stroke=\"{stroke.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"stroke-width=\"{_strokeWidth.ToString(CultureInfo.InvariantCulture)}\" />");
    }

    public void DrawEllipse(Point center, double rx, double ry)
    {
        string stroke = FormatColor(_strokeColor);
        _writer.WriteLine($"<ellipse cx=\"{center.X.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"cy=\"{center.Y.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"rx=\"{rx.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"ry=\"{ry.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"stroke=\"{stroke.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"stroke-width=\"{_strokeWidth.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"fill=\"none\" />");
    }

    public void FillEllipse(Point center, double rx, double ry)
    {
        string fill = FormatColor(_fillColor);
        _writer.WriteLine($"<ellipse cx=\"{center.X.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"cy=\"{center.Y.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"rx=\"{rx.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"ry=\"{ry.ToString(CultureInfo.InvariantCulture)}\" " +
                          $"fill=\"{fill}\" />");
    }

    public void FillPolygon(Point[] points)
    {
        if (points.Length < 2)
        {
            return;
        }

        string fill = FormatColor(_fillColor);
        string pointsStr = string.Join(" ", 
            points.Select(p => $"{p.X.ToString(CultureInfo.InvariantCulture)}," +
                               $"{p.Y.ToString(CultureInfo.InvariantCulture)}"));
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

    public void Dispose()
    {
        _writer.WriteLine("</svg>");
    }

    private string FormatColor(RgbaColor color)
    {
        int r = (int)color.R;
        int g = (int)color.G;
        int b = (int)color.B;
        double a = color.A;
        return
            $"rgba({r.ToString(CultureInfo.InvariantCulture)}, " +
            $"{g.ToString(CultureInfo.InvariantCulture)}, " +
            $"{b.ToString(CultureInfo.InvariantCulture)}, " +
            $"{a.ToString(CultureInfo.InvariantCulture)})";
    }

    private double Clamp(double value)
    {
        return Math.Max(0, Math.Min(255, value));
    }
}