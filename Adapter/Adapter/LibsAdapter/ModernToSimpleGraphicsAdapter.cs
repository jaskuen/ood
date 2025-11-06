using Adapter.GraphicsLib;
using Adapter.ModernGraphicsLib;
using Point = Adapter.ShapeDrawingLib.Point;

namespace Adapter.LibsAdapter;

public class ModernToSimpleGraphicsAdapter : ICanvas
{
    private readonly ModernGraphicsRenderer _renderer;
    private Point _current = new(0, 0);
    private readonly RgbaColor _color = new(0, 0, 0, 1);
    
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
        _renderer.DrawLine(_current, new Point(x, y), _color);
    }

    public void SetColor(int color)
    {
        int red = (color >> 16) & 0xFF;
        int green = (color >> 8) & 0xFF;
        int blue = color & 0xFF;
        
        _color.R = red / 255f;
        _color.G = green / 255f;
        _color.B = blue / 255f;
    }
}