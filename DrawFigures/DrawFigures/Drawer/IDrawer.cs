using DrawFigures.Canvas;

namespace DrawFigures.Drawer;

public interface IDrawer
{
    public void Draw(ICanvas canvas);
}