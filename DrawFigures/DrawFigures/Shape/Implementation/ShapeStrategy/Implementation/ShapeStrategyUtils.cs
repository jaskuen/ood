using DrawFigures.Picture.Implementation;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public static class ShapeStrategyUtils
{
    private static IDictionary<string, Func<IList<string>, IShapeStrategy>> _shapeToCtor =
        new Dictionary<string, Func<IList<string>, IShapeStrategy>>()
        {
            {
                "circle", parameters =>
                    new CircleStrategy(
                        parameters[0],
                        parameters[1],
                        parameters[2])
            },
            {
                "rectangle", parameters =>
                    new RectangleStrategy(
                        parameters[0],
                        parameters[1],
                        parameters[2],
                        parameters[3]
                    )
            },
            {
                "triangle", parameters =>
                    new TriangleStrategy(
                        parameters[0],
                        parameters[1],
                        parameters[2],
                        parameters[3],
                        parameters[4],
                        parameters[5]
                    )
            },
            {
                "line", parameters =>
                    new LineStrategy(
                        parameters[0],
                        parameters[1],
                        parameters[2],
                        parameters[3]
                    )
            },
            {
                "text", parameters =>
                    new TextStrategy(
                        parameters[0],
                        parameters[1],
                        parameters[2],
                        parameters[3]
                    )
            }
        };

    public static IShapeStrategy CreateShapeStrategy(string shape, IList<string> parameters)
    {
        if (!_shapeToCtor.ContainsKey(shape))
        {
            throw new ArgumentException("Unknown shape type");
        }

        return _shapeToCtor[shape](parameters);
    }

    public static void AddNewShape(string shapeName, Func<IList<string>, IShapeStrategy> creator)
    {
        if (!_shapeToCtor.TryAdd(shapeName, creator))
        {
            throw new ArgumentException("Failed to create new shape: shape with this name already exists");
        }
    }
}