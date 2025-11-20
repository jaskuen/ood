namespace Proxy.Lib.Drawing;

public interface IDrawer
{
    public void DrawLine(Image image, Point from, Point to, char color);
    public void DrawCircle(Image image, Point center, int radius, char color);
    public void FillCircle(Image image, Point center, int radius, char color);
}