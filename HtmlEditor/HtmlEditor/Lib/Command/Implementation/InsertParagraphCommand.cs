using HtmlEditor.Lib.Document;
using HtmlEditor.Lib.Document.Paragr;

namespace HtmlEditor.Lib.Command.Implementation;

public class InsertParagraphCommand : AbstractCommand
{
    private IList<DocumentItem> _items;
    private readonly int _position;
    private readonly string _text;
    
    public InsertParagraphCommand( IList<DocumentItem> items, int position, string text)
    {
        _items = items;
        _position = position;
        _text = text;
    }

    public override bool Merge(ICommand other) => false;

    public override void Destroy()
    {
    }

    protected override void DoExecute()
    {
        Paragraph paragraph = new Paragraph(_text);
        DocumentItem documentItem = new DocumentItem(paragraph);

        if (_position >= 0)
        {
            _items.Insert(_position, documentItem);
            return;
        }
        
        _items.Add(documentItem);
    }

    protected override void DoUndo()
    {
        _items.RemoveAt(_position);
    }
}