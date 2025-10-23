using HtmlEditor.Lib.Document.Img;

namespace HtmlEditor.Lib.Document;

public interface IDocument
{
    /// <summary>
    /// Вставляет параграф текста в указанную позицию (сдвигая последующие элементы)
    /// Если параметр position не указан, вставка происходит в конец документа
    /// </summary>
    /// <param name="text"></param>
    /// <param name="position"></param>
    public void InsertParagraph(string text, int? position = null);
    /// <summary>
    /// Заменяет текст в параграфе, находящемся в указанной позиции документа.
    /// Если в данной позиции не находится параграф, выдается сообщение об ошибке, а команда игнорируется.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="text"></param>
    public void ReplaceText(int position, string text);
    /// <summary>
    /// Вставляет изображение в указанную позицию (сдвигая последующие элементы)
    /// Параметр path задает путь к вставляемому изображению
    /// При вставке изображение должно копироваться в подкаталог images
    /// под автоматически сгенерированным именем
    /// </summary>
    /// <param name="path"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="position"></param>
    public void InsertImage(string path, int width, int height, int? position = null);
    /// <summary>
    /// Изменяет размер изображения, находящегося в указанной позиции документа.
    /// Если в данной позиции не находится изображение, выдается сообщение об ошибке
    /// </summary>
    /// <param name="position"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public void ResizeImage(int position, int width, int height);
    /// <summary>
    /// Возвращает количество элементов в документе
    /// </summary>
    /// <returns></returns>
    public int GetItemsCount();
    /// <summary>
    /// Доступ к элементам изображения
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public DocumentItem GetItem(int index);
    public ConstDocumentItem GetItemConst(int index);
    /// <summary>
    /// Удаляет элемент из документа
    /// </summary>
    /// <param name="index"></param>
    public void DeleteItem(int index);
    /// <summary>
    /// Возвращает заголовок документа
    /// </summary>
    /// <returns></returns>
    public string GetTitle();
    /// <summary>
    /// Изменяет заголовок документа
    /// </summary>
    /// <param name="title"></param>
    public void SetTitle(string title);
    /// <summary>
    /// Сообщает о доступности операции Undo
    /// </summary>
    /// <returns></returns>
    public bool CanUndo();
    /// <summary>
    /// Отменяет команду редактирования
    /// </summary>
    public void Undo();
    /// <summary>
    /// Сообщает о доступности операции Redo
    /// </summary>
    /// <returns></returns>
    public bool CanRedo();
    /// <summary>
    /// Выполняет отмененную команду редактирования
    /// </summary>
    public void Redo();
    /// <summary>
    /// Сохраняет документ в формате html. Изображения сохраняются в подкаталог images.
    /// Пути к изображениям указываются относительно пути к сохраняемому HTML файлу
    /// </summary>
    /// <param name="path"></param>
    public void Save(string path);
}