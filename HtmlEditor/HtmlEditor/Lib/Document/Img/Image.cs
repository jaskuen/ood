namespace HtmlEditor.Lib.Document.Img;

public class Image : IImage
{
    private int _width;
    private int _height;
    private readonly string _path;

    public Image(int width, int height, string path)
    {
        _width = width;
        _height = height;
        _path = path;
    }

    public string GetPath()
    {
        return _path;
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }

    public virtual void Resize(int width, int height)
    {
        _width = width;
        _height = height;
    }
}