using Adapter.GraphicsLib;
using Adapter.ModernGraphicsLib;
using Adapter.ShapeDrawingLib;

namespace Adapter.LibsAdapter;

public class ModernToSimpleGraphicsAdapter : ICanvas
{
    private readonly ModernGraphicsRenderer _renderer;
    private Point _current = new(0, 0);
    
    public ModernToSimpleGraphicsAdapter(ModernGraphicsRenderer renderer)
    {
        _renderer = renderer;
        renderer.BeginDraw();
    }
    
    public void MoveTo(int x, int y)
    {
        _current.X = x;
        _current.Y = y;
    }

    public void LineTo(int x, int y)
    {
        _renderer.DrawLine(_current, new Point(x, y));
    }
}