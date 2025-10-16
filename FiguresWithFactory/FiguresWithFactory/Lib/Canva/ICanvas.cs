namespace FiguresWithFactory.Lib.Canva;

public interface ICanvas
{
    public void SetColor(Color color);
    public void DrawLine(double x1, double y1, double x2, double y2);
    public void DrawEllipse(double cx, double cy, double rx, double ry);
    public string GetDrawing();
}