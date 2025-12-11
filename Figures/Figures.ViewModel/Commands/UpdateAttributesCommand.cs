using Figures.Model;
using Figures.Model.Core;
using Figures.Model.Document;

namespace Figures.ViewModel.Commands;

/// <summary>
/// Команда для обновления атрибутов стиля фигуры.
/// </summary>
public class UpdateAttributesCommand(
    DocumentModel document,
    string figureId,
    FigureAttributes from,
    FigureAttributes to)
    : ICommand
{
    public string FigureId => figureId;

    public void Execute()
    {
        document.UpdateFigure(figureId, figure => figure.SetAttributes(to), "attributes");
    }

    public void Undo()
    {
        document.UpdateFigure(figureId, figure => figure.SetAttributes(from), "attributes");
    }
}

