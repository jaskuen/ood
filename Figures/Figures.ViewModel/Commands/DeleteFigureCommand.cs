using Figures.Model.Document;
using Figures.Model.Figures;

namespace Figures.ViewModel.Commands;

/// <summary>
/// Команда для удаления фигуры из документа.
/// </summary>
public class DeleteFigureCommand : ICommand
{
    private readonly DocumentModel _document;
    private FigureModel? _removedFigure;
    private readonly int _index;

    public string FigureId { get; }

    public DeleteFigureCommand(DocumentModel document, FigureModel figure)
    {
        _document = document;
        FigureId = figure.Id;
        _removedFigure = figure;
        _index = _document.Figures.FindIndex(f => f.Id == figure.Id);
    }

    public void Execute()
    {
        _removedFigure = _document.RemoveFigure(FigureId);
    }

    public void Undo()
    {
        if (_removedFigure != null)
        {
            _document.AddFigure(_removedFigure, _index);
        }
    }
}

