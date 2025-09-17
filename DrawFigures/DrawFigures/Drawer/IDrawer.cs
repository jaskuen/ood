using SkiaSharp;

namespace DrawFigures.Drawer;

public interface IDrawer
{
    public void Draw(SKBitmap bitmap);
}