namespace Adapter.ModernGraphicsLib;

public class RgbaColor
{
    public float R;
    public float G;
    public float B;
    public readonly float A;

    public RgbaColor(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}