namespace Figures.Model.Core.Events;

/// <summary>
/// Типы событий, используемых в системе событий приложения.
/// </summary>
public enum EventType
{
    // DocumentModel

    /// <summary>
    /// Событие изменения имени документа.
    /// </summary>
    NameChanged,

    /// <summary>
    /// Событие изменения размера холста.
    /// </summary>
    CanvasChanged,

    /// <summary>
    /// Событие добавления фигуры в документ.
    /// </summary>
    FigureAdded,

    /// <summary>
    /// Событие удаления фигуры из документа.
    /// </summary>
    FigureRemoved,

    /// <summary>
    /// Событие изменения свойств фигуры.
    /// </summary>
    FigureChanged,

    /// <summary>
    /// Событие очистки документа.
    /// </summary>
    Cleared,

    // FigureModel

    /// <summary>
    /// Событие изменения атрибутов фигуры (цвет, обводка и т.д.).
    /// </summary>
    AttributesChanged,

    /// <summary>
    /// Событие изменения геометрии фигуры (позиция, размер).
    /// </summary>
    GeometryChanged,

    // ImageModel events
    /// <summary>
    /// Событие изменения изображения.
    /// </summary>
    ImageChanged,

    // Selection events
    /// <summary>
    /// Событие изменения выделения.
    /// </summary>
    SelectionChanged,

    // DocumentEditor events
    /// <summary>
    /// Событие сохранения документа.
    /// </summary>
    Saved,

    /// <summary>
    /// Событие загрузки документа.
    /// </summary>
    Loaded,

    /// <summary>
    /// Событие применения команды (выполнение/отмена/повтор).
    /// </summary>
    CommandApplied,

    // WindowManager events
    /// <summary>
    /// Событие создания нового окна.
    /// </summary>
    WindowCreated,

    /// <summary>
    /// Событие закрытия окна.
    /// </summary>
    WindowClosed,

    /// <summary>
    /// Событие изменения активного окна.
    /// </summary>
    ActiveWindowChanged,

    // CommandHistory events
    /// <summary>
    /// Событие изменения состояния истории команд (можно/нельзя отменить/повторить).
    /// </summary>
    Change
}