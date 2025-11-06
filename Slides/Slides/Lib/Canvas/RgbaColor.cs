namespace Slides.Lib.Canvas;

public class RgbaColor
{
    public double R { get; set; }
    public double G { get; set; }
    public double B { get; set; }
    public double A { get; set; }

    public RgbaColor(double r, double g, double b, double a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}