using FiguresWithFactory.Lib.Canva;
using FiguresWithFactory.Lib.Canva.Implementation;
using FiguresWithFactory.Lib.Shapes;
using FiguresWithFactory.Lib.Shapes.Factory;
using FiguresWithFactory.Lib.Shapes.Factory.Implementation;
using FiguresWithFactory.Lib.Shapes.Inheritors;

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
            "<ellipse cx=\"200\" cy=\"150\" rx=\"100\" ry=\"50\" fill=\"none\" stroke=\"yellow\" stroke-width=\"3\" />";
        
        Assert.DoesNotThrow(() => canvas = new Canvas(1000, 500));
        Assert.That(canvas!.GetDrawing(), Is.EqualTo(start + end));
        
        canvas.SetColor(Color.Red);
        canvas.DrawLine(50, 250, 350, 50);
        Assert.That(canvas.GetDrawing(), Is.EqualTo(start + line + end));
        
        canvas.SetColor(Color.Yellow);
        canvas.DrawEllipse(200, 150, 100, 50);
        Assert.That(canvas.GetDrawing(), Is.EqualTo(start + line + ellipse + end));
    }

    [Test]
    public void Factory_CreateFigures_ResultsAreCorrect()
    {
        IShapeFactory factory = new ShapeFactory();

        string rectangleCommand = "rectangle black 100 100 200 200";
        string ellipseCommand = "ellipse red 100 200 300 50";
        string regularCommand = "regular green 5 100 200 300";
        string triangleCommand = "triangle blue 100 100 200 200 300 300";

        Shape rect = factory.CreateShape(rectangleCommand);
        Shape ellipse = factory.CreateShape(ellipseCommand);
        Shape triangle = factory.CreateShape(triangleCommand);
        Shape regular = factory.CreateShape(regularCommand);
        
        Assert.That(rect.GetType(), Is.EqualTo(typeof(Rectangle)));
        Assert.That(ellipse.GetType(), Is.EqualTo(typeof(Ellipse)));
        Assert.That(triangle.GetType(), Is.EqualTo(typeof(Triangle)));
        Assert.That(regular.GetType(), Is.EqualTo(typeof(RegularPolygon)));
    }
}