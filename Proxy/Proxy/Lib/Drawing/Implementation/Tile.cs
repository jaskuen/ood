namespace Proxy.Lib.Drawing.Implementation;

public sealed class Tile : ITile
{
    private const int SIZE = 8;

    private readonly char[] _pixels = new char[SIZE * SIZE];

    private static int _sInstanceCount = 0;
    internal static int InstanceCount => _sInstanceCount;

    internal static void ClearInstancesCount()
    {
        _sInstanceCount = 0;
    }

    public Tile(char fillChar = ' ')
    {
        for (int i = 0; i < _pixels.Length; i++)
        {
            _pixels[i] = fillChar;
        }

        Interlocked.Increment(ref _sInstanceCount);
    }

    public void Dispose()
    {
        Interlocked.Decrement(ref _sInstanceCount);
    }

    private Tile(char[] pixels)
    {
        _pixels = pixels;
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