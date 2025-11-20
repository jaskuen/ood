namespace Proxy.Lib.Drawing;

public interface ITile : IDisposable, ICloneable
{
    const int SIZE = 8;

    char GetPixel(Point p);
    void SetPixel(Point p, char color);
}