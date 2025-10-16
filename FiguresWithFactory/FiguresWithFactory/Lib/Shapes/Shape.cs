using FiguresWithFactory.Lib.Canva;

namespace FiguresWithFactory.Lib.Shapes;

public abstract class Shape
{
    private readonly Color _color;

    public Shape(Color color)
    {
        _color = color;
    }

    public void Draw(ICanvas canvas)
    {
        canvas.SetColor(_color);
        DrawFigure(canvas);
    }

    protected abstract void DrawFigure(ICanvas canvas);
    public Color GetColor() => _color;
}