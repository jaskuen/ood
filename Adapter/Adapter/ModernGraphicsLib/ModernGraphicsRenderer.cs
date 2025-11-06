using Adapter.ShapeDrawingLib;

namespace Adapter.ModernGraphicsLib;

using System;
using System.IO;

public class ModernGraphicsRenderer : IDisposable
{
    private readonly TextWriter _out;
    private bool _drawing = false;

    public ModernGraphicsRenderer(TextWriter output)
    {
        _out = output;
    }

    public void Dispose()
    {
        if (_drawing)
        {
            EndDraw();
        }
    }

    public void BeginDraw()
    {
        if (_drawing)
        {
            throw new InvalidOperationException("Drawing has already begun");
        }
        _out.WriteLine("<draw>");
        _drawing = true;
    }

    public void DrawLine(Point start, Point end)
    {
        if (!_drawing)
        {
            throw new InvalidOperationException("DrawLine is allowed between BeginDraw()/EndDraw() only");
        }
        _out.WriteLine($"  <line fromX=\"{start.X}\" fromY=\"{start.Y}\" toX=\"{end.X}\" toY=\"{end.Y}\"/>");
    }

    public void EndDraw()
    {
        if (!_drawing)
        {
            throw new InvalidOperationException("Drawing has not been started");
        }
        _out.WriteLine("</draw>");
        _drawing = false;
    }
}