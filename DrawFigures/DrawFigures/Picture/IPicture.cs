namespace DrawFigures.Picture;

public interface IPicture
{
    public void DrawFigure(string id);
    public void DrawPicture();
    public void AddShape(string id, string color, string shape, string parameters);
    public void DeleteShape(string id);
    public void List();
}