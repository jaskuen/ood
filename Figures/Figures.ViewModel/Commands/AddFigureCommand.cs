using Figures.Model.Document;
using Figures.Model.Figures;

namespace Figures.ViewModel.Commands;

/// <summary>
/// Команда для добавления фигуры в документ.
/// </summary>
public class AddFigureCommand(DocumentModel document, FigureModel figure) : ICommand
{
    public string FigureId { get; } = figure.Id;

    public void Execute()
    {
        document.AddFigure(figure);
    }

    public void Undo()
    {
        document.RemoveFigure(FigureId);
    }
}

