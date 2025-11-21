namespace Proxy.Lib.Drawing;

public interface ITile : ICloneable, IDisposable
{
    const int SIZE = 8;

    char GetPixel(Point p);
    void SetPixel(Point p, char color);
}