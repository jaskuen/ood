namespace HtmlEditor.Lib.Document.Img;

public class ConstImage : IImage
{
    private IImage _image;

    public ConstImage(IImage image)
    {
        _image = image;
    }


    public string GetPath() => _image.GetPath();

    public int GetWidth() => _image.GetWidth();

    public int GetHeight() => _image.GetHeight();

    public void Resize(int width, int height)
    {
    }
}