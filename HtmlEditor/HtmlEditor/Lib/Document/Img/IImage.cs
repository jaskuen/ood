namespace HtmlEditor.Lib.Document.Img;

public interface IImage
{
    public string GetPath();
    public int GetWidth();
    public int GetHeight();
    public void Resize(int width, int height);
}