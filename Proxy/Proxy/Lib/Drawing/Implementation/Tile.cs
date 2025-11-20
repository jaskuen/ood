namespace Proxy.Lib.Drawing.Implementation;

public sealed class Tile : ITile
{
    private const int SIZE = 8;

    private readonly char[] _pixels = new char[SIZE * SIZE];

    // Подсчёт именно реальных Tile (как в оригинальном C++ задании)
    private static int s_instanceCount = 0;
    public static int InstanceCount => s_instanceCount;

    public Tile(char fillChar = ' ')
    {
        for (int i = 0; i < _pixels.Length; i++)
        {
            _pixels[i] = fillChar;
        }

        Interlocked.Increment(ref s_instanceCount);
    }

    private Tile(char[] pixels)
    {
        _pixels = pixels;
    }

    public void Dispose()
    {
        Interlocked.Decrement(ref s_instanceCount);
    }

    public char GetPixel(Point p) =>
        (p.X is >= 0 and < SIZE && p.Y is >= 0 and < SIZE) ? _pixels[p.Y * SIZE + p.X] : ' ';

    public void SetPixel(Point p, char color)
    {
        if (p.X is >= 0 and < SIZE && p.Y is >= 0 and < SIZE)
        {
            _pixels[p.Y * SIZE + p.X] = color;
        }
    }

    public object Clone()
    {
        char[] pixels = new char[SIZE * SIZE];
        _pixels.CopyTo(pixels, 0);
        return new Tile(pixels);
    }
}