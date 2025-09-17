using DrawFigures.Picture;
using DrawFigures.Shape;

namespace DrawFigures.CommandExecutor.Implementation.CommandStrategy.Implementation;

public class MovePictureStrategy : ICommandStrategy
{
    private IPicture _picture;
    private List<string> _parameters;

    public MovePictureStrategy(IPicture picture, List<string> parameters)
    {
        _picture = picture;
        _parameters = parameters;
    }

    public void ExecuteCommand()
    {
        List<IShape> shapes = _picture.GetShapes();

        foreach (IShape shape in shapes)
        {
            shape.Move(float.Parse(_parameters[0]), float.Parse(_parameters[1]));
        }
    }
}