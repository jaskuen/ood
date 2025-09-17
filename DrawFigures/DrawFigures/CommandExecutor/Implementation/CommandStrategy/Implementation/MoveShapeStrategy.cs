using DrawFigures.Picture;
using DrawFigures.Shape;

namespace DrawFigures.CommandExecutor.Implementation.CommandStrategy.Implementation;

public class MoveShapeStrategy : ICommandStrategy
{
    private IPicture _picture;
    private List<string> _parameters;

    public MoveShapeStrategy(IPicture picture, List<string> parameters)
    {
        _picture = picture;
        _parameters = parameters;
    }

    // Command parameters: <id> <dx> <dy>
    public void ExecuteCommand()
    {
        IShape shape = _picture.GetShape(_parameters[0]);

        shape.Move(float.Parse(_parameters[1]), float.Parse(_parameters[2]));
    }
}