using DrawFigures.Picture;
using DrawFigures.Picture.Implementation;
using DrawFigures.Shapes.Implementation.ShapeStrategy;
using DrawFigures.Shapes.Implementation.ShapeStrategy.Implementation;

namespace DrawFigures.CommandExecutor.Implementation;

public class ConsoleCommandExecutor : ICommandExecutor
{
    private IPicture _picture;

    public ConsoleCommandExecutor(IPicture picture)
    {
        _picture = picture;
    }

    public bool ExecuteCommand(string command)
    {
        var parsedCommand = command.Split(' ').ToList();

        var commandName = parsedCommand[0].ToLower();
        parsedCommand.RemoveAt(0);
        // command name
        switch (commandName)
        {
            // <id> <color> <type> <params> 
            case "addshape":
                var shapeToAdd =
                    new Shapes.Implementation.Shape(parsedCommand[1], _picture.GetCanvas());
                shapeToAdd.SetShapeStrategy(
                    ShapeStrategyUtils.CreateShapeStrategy(parsedCommand[2],
                        parsedCommand.GetRange(3, parsedCommand.Count - 3))
                );
                _picture.AddShape(parsedCommand[0], shapeToAdd);
                break;

            // <id> <dx> <dy>
            case "moveshape":
                _picture.MoveShape(parsedCommand[0], parsedCommand[1].ToFloat(), parsedCommand[2].ToFloat());
                break;

            case "movepicture":
                _picture.MovePicture(parsedCommand[0].ToFloat(), parsedCommand[1].ToFloat());
                break;

            case "deleteshape":
                _picture.DeleteShape(parsedCommand[0]);
                break;

            case "list":
                _picture.List();
                break;

            case "changecolor":
                _picture.ChangeShapeColor(parsedCommand[0], parsedCommand[1]);
                break;

            case "changeshape":
                IShapeStrategy newShapeStrategy = ShapeStrategyUtils.CreateShapeStrategy(parsedCommand[1],
                    parsedCommand.GetRange(2, parsedCommand.Count - 2));
                _picture.ChangeShapeType(parsedCommand[0], newShapeStrategy);
                break;

            case "drawshape":
                _picture.DrawFigure(parsedCommand[0]);
                break;

            case "drawpicture":
                _picture.DrawPicture();
                break;

            case "exit":
                return true;
        }

        return false;
    }
}