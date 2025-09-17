using DrawFigures.Picture.Implementation;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public static class ShapeStrategyUtils
{
    public static IShapeStrategy CreateShapeStrategy(string shape, List<string> parameters)
    {
        switch (shape)
        {
            case "circle":
                return new CircleStrategy(
                    parameters[0].ToFloat(),
                    parameters[1].ToFloat(),
                    parameters[2].ToFloat());
            case "rectangle":
                return new RectangleStrategy(
                    parameters[0].ToFloat(),
                    parameters[1].ToFloat(),
                    parameters[2].ToFloat(),
                    parameters[3].ToFloat()
                );
            case "triangle":
                return new TriangleStrategy(
                    parameters[0].ToFloat(),
                    parameters[1].ToFloat(),
                    parameters[2].ToFloat(),
                    parameters[3].ToFloat(),
                    parameters[4].ToFloat(),
                    parameters[5].ToFloat()
                );
            case "line":
                return new LineStrategy(
                    parameters[0].ToFloat(),
                    parameters[1].ToFloat(),
                    parameters[2].ToFloat(),
                    parameters[3].ToFloat()
                );
            case "text":
                return new TextStrategy(
                    parameters[0].ToFloat(),
                    parameters[1].ToFloat(),
                    parameters[2],
                    parameters[3].ToFloat()
                );
            default:
                throw new ArgumentException("Unknown shape type");
        }
    }
}