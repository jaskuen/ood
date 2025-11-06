using Adapter.GraphicsLib;
using Adapter.GraphicsLib.Implementation;
using Adapter.LibsAdapter;
using Adapter.ModernGraphicsLib;
using Adapter.ShapeDrawingLib;
using Adapter.ShapeDrawingLib.Implementation;

namespace Adapter;

public static class Program
{
    private static void PaintPicture(CanvasPainter painter)
    {
        Triangle triangle = new Triangle(new(10, 15), new Point(100, 200), new(150, 250));
        Rectangle rectangle = new Rectangle(new(30, 40), 18, 24);
        painter.Draw(triangle);
        painter.Draw(rectangle);
    }

    private static void PaintPictureOnCanvas()
    {
        ICanvas canvas = new Canvas();
        CanvasPainter painter = new CanvasPainter(canvas);
        PaintPicture(painter);
    }

    private static void PaintPictureOnModernGraphicsRenderer()
    {
        using ModernGraphicsRenderer renderer = new ModernGraphicsRenderer(Console.Out);
        ICanvas canvas = new ModernToSimpleGraphicsAdapter(renderer);
        CanvasPainter painter = new CanvasPainter(canvas);
        PaintPicture(painter);
    }

    public static void Main()
    {
        Console.WriteLine("Should we use new API (y)?");
        string userInput;
        if (!string.IsNullOrWhiteSpace(userInput = Console.ReadLine()!.ToLower()) && userInput == "y")
        {
            PaintPictureOnModernGraphicsRenderer();
        }
        else
        {
            PaintPictureOnCanvas();
        }
    }
}