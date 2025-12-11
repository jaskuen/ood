using Figures.Model;
using Figures.Model.Core;
using Figures.Model.Document;

namespace Figures.ViewModel.Commands;

/// <summary>
/// Команда для трансформации (перемещения или изменения размера) фигуры.
/// </summary>
public class TransformFigureCommand(DocumentModel document, string figureId, Rect from, Rect to)
    : ICommand
{
    public string FigureId => figureId;

    public void Execute()
    {
        document.UpdateFigure(figureId, figure => figure.Resize(to), "geometry");
    }

    public void Undo()
    {
        document.UpdateFigure(figureId, figure => figure.Resize(from), "geometry");
    }
}

