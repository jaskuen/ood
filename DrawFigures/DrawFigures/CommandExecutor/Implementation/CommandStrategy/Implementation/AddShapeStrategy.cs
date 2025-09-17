using DrawFigures.Picture;
using DrawFigures.Shape.Implementation;

namespace DrawFigures.CommandExecutor.Implementation.CommandStrategy.Implementation;

public class AddShapeStrategy : ICommandStrategy
{
    private IPicture _picture;
    private List<string> _parameters;

    public AddShapeStrategy(IPicture picture, List<string> parameters)
    {
        _picture = picture;
        _parameters = parameters;
    }

    public void ExecuteCommand()
    {
    }
}