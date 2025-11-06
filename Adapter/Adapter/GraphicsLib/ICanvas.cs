namespace Adapter.GraphicsLib;

public interface ICanvas
{
    public void MoveTo(int x, int y);
    public void LineTo(int x, int y);
    public void SetColor(int color);
}