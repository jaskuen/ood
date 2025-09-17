using SkiaSharp;

namespace DrawFigures.Drawer.Implementation;

public class FileDrawer : IDrawer
{
    private string _filePath;

    public FileDrawer(string filePath)
    {
        _filePath = filePath;
    }

    public void Draw(SKBitmap bitmap)
    {
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite(_filePath);
        data.SaveTo(stream);
    }
}