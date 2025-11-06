using System.Drawing;

namespace Slides.Lib.Canvas;

public struct Point
{
    public double X;
    public double Y;

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
}

public interface ICanvas
{
    public void DrawLine(Point p1, Point p2);
    public void DrawEllipse(Point center, double rx, double ry);
    public void FillEllipse(Point center, double rx, double ry);
    public void FillPolygon(Point[] points);
    public void SetFillColor(double r, double g, double b, double a);
    public void SetStrokeColor(double r, double g, double b, double a);
    public void SetStrokeWidth(double w);
}