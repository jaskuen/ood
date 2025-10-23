using HtmlEditor.Lib.Document;
using HtmlEditor.Lib.Document.Paragr;

namespace HtmlEditor.Lib.Command.Implementation;

public class ReplaceTextCommand : AbstractCommand
{
    private IList<DocumentItem> _items;
    private readonly int _position;
    private string _newText;
    private string _oldText;

    public ReplaceTextCommand(IList<DocumentItem> items, int position, string text)
    {
        _items = items;
        _position = position;
        _newText = text;
    }

    public override bool Merge(ICommand other)
    {
        if (other is ReplaceTextCommand otherCommand)
        {
            DocumentItem item = _items[_position];
            IParagraph? paragraph = item.GetParagraph();

            if (paragraph == null)
            {
                throw new Exception("Item is not a paragraph");
            }

            _newText = otherCommand._newText;

            paragraph.SetText(_newText);

            return true;
        }
        
        return false;
    }

    public override void Destroy()
    {
    }

    protected override void DoExecute()
    {
        if (_position >= _items.Count)
        {
            throw new Exception("Position out of range");
        }

        IParagraph? paragraph = _items[_position].GetParagraph();

        if (paragraph == null)
        {
            throw new Exception("Item is not a paragraph");
        }

        _oldText = paragraph.GetText();
        paragraph.SetText(_newText);
    }

    protected override void DoUndo()
    {
        if (_position >= _items.Count)
        {
            throw new Exception("Position out of range");
        }

        IParagraph? paragraph = _items[_position].GetParagraph();

        if (paragraph == null)
        {
            throw new Exception("Item is not a paragraph");
        }

        _newText = paragraph.GetText();
        paragraph.SetText(_oldText);
    }
}