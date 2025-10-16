using FiguresWithFactory.Lib.Shapes.Factory;
using FiguresWithFactory.Lib.Shapes.Factory.Implementation;

namespace FiguresWithFactory.Lib.Shapes;

public class Designer
{
    private readonly IShapeFactory _factory = new ShapeFactory();

    public PictureDraft CreateDraft(Stream stream)
    {
        IList<Shape> shapes = new List<Shape>();

        StreamReader sr = new StreamReader(stream);
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            line = line?.Trim().ToLower();
            if (line is null or "exit")
            {
                break;
            }

            shapes.Add(_factory.CreateShape(line));
        }

        return new PictureDraft(shapes);
    }
}