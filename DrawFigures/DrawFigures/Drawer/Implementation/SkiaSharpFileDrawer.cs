using DrawFigures.Canvas;
using SkiaSharp;

namespace DrawFigures.Drawer.Implementation;

public class SkiaSharpFileDrawer : IDrawer
{
    private string _filePath;

    public SkiaSharpFileDrawer(string filePath)
    {
        _filePath = filePath;
    }

    public void Draw(ICanvas canvas)
    {
        var bitmap = canvas.GetBitmap();
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite(_filePath);
        data.SaveTo(stream);
    }
}