using HtmlEditor.Extensions;
using HtmlEditor.Lib.Command.Implementation;

namespace HtmlEditor.Lib.Document.Implementation;

public class Document : IDocument
{
    private IList<DocumentItem> _items = [];
    private Ref<string> _title = new("");

    private readonly History _history = new();

    public void InsertParagraph(string text, int? position = null)
    {
        _history.AddAndExecuteCommand(
            new InsertParagraphCommand(_items, position ?? _items.Count, text)
        );
    }

    public void ReplaceText(int position, string text)
    {
        _history.AddAndExecuteCommand(
            new ReplaceTextCommand(_items, position, text)
        );
    }

    public void InsertImage(string path, int width, int height, int? position = null)
    {
        _history.AddAndExecuteCommand(
            new InsertImageCommand(_items, position ?? _items.Count, width, height, path)
        );
    }

    public void ResizeImage(int position, int width, int height)
    {
        _history.AddAndExecuteCommand(
            new ResizeImageCommand(_items, position, width, height)
        );
    }

    public int GetItemsCount()
    {
        return _items.Count;
    }

    public DocumentItem GetItem(int index)
    {
        return _items[index];
    }

    public ConstDocumentItem GetItemConst(int index)
    {
        return _items[index];
    }

    public void DeleteItem(int index)
    {
        _history.AddAndExecuteCommand(
            new DeleteItemCommand(_items, index)
        );
    }

    public string GetTitle()
    {
        return _title.Value;
    }

    public void SetTitle(string title)
    {
        _history.AddAndExecuteCommand(
            new SetTitleCommand(_title, title)
        );
    }

    public bool CanUndo() => _history.CanUndo();

    public void Undo() => _history.Undo();

    public bool CanRedo() => _history.CanRedo();

    public void Redo() => _history.Redo();

    public void Save(string path)
    {
        HtmlExtensions.CreateHtmlDocument(this, path);
    }
}