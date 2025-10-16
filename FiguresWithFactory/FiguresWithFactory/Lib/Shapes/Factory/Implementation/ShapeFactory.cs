using FiguresWithFactory.Lib.Shapes.Inheritors;

namespace FiguresWithFactory.Lib.Shapes.Factory.Implementation;

public class ShapeFactory : IShapeFactory
{
    public Shape CreateShape(string description)
    {
        // args: <shape type> <color> <shape params>
        string[] args = description.Split(' ');

        switch (args[0].ToLower())
        {
            // <color> <x1> <y1> <x2> <y2>
            case "rectangle":
                return new Rectangle(args[1].ParseToColor(), args[2].ParseToDouble(), args[3].ParseToDouble(),
                    args[4].ParseToDouble(), args[5].ParseToDouble());

            // <color> <x1> <y1> <x2> <y2> <x3> <y3>
            case "triangle":
                return new Triangle(args[1].ParseToColor(), args[2].ParseToDouble(), args[3].ParseToDouble(),
                    args[4].ParseToDouble(), args[5].ParseToDouble(), args[6].ParseToDouble(), args[7].ParseToDouble());

            // <color> <cx> <cy> <rx> <ry>
            case "ellipse":
                return new Ellipse(args[1].ParseToColor(), args[2].ParseToDouble(), args[3].ParseToDouble(),
                    args[4].ParseToDouble(), args[5].ParseToDouble());

            // <color> <cx> <cy> <vertex count> <radius>
            case "regular":
                return new RegularPolygon(args[1].ParseToColor(), args[2].ParseToDouble(), args[3].ParseToDouble(),
                    args[4].ParseToInt(), args[5].ParseToDouble());
            
            default: throw new ArgumentException("Unknown shape type");
        }
    }
}