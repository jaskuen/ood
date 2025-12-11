namespace Figures.ViewModel.Commands;

/// <summary>
/// Интерфейс для реализации паттерна "Команда".
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Выполняет действие команды.
    /// </summary>
    void Execute();

    /// <summary>
    /// Отменяет действие команды.
    /// </summary>
    void Undo();
}

