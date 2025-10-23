namespace HtmlEditor.Lib.Document.Img;

public class ImagePath
{
    private readonly string _originalPath;
    private readonly string _name;

    public ImagePath(string originalPath)
    {
        _originalPath = originalPath;
        _name = $"{Path.GetFileName(_originalPath)}_{Guid.NewGuid()}";
    }
    
    public string GetName() => _name;
    public string GetOriginalPath() => _originalPath;
}