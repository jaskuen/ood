using FiguresWithFactory.Lib.Canva;
using FiguresWithFactory.Lib.Canva.Implementation;

namespace FiguresWithFactoryTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Canvas_CreateCanvasAndFillWithFigures_ResultsAreCorrect()
    {
        ICanvas? canvas = null;
        string start = "<svg width=\"1000px\" height=\"500px\">";
        string end = "</svg>";
        
        string line = "<line x1=\"50\" y1=\"250\" x2=\"350\" y2=\"50\" stroke=\"red\" stroke-width=\"3\" />";
        string ellipse =
            "<ellipse cx=\"200\" cy=\"150\" rx=\"100\" ry=\"50\" stroke=\"yellow\" stroke-width=\"3\" />";
        
        Assert.DoesNotThrow(() => canvas = new Canvas(1000, 500));
        Assert.That(canvas!.GetDrawing(), Is.EqualTo(start + end));
        
        canvas.SetColor(Color.Red);
        canvas.DrawLine(50, 250, 350, 50);
        Assert.That(canvas.GetDrawing(), Is.EqualTo(start + line + end));
        
        canvas.SetColor(Color.Yellow);
        canvas.DrawEllipse(200, 150, 100, 50);
        Assert.That(canvas.GetDrawing(), Is.EqualTo(start + line + ellipse + end));
    }
}