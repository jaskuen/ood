using HtmlEditor.Lib.Document;

namespace HtmlEditor.Lib.Command.Implementation;

public class DeleteItemCommand : AbstractCommand
{
    private IList<DocumentItem>  _items;
    private DocumentItem? _item;
    private readonly int _position;

    public DeleteItemCommand(IList<DocumentItem> items, int position)
    {
        _items = items;
        _position = position;
    }

    public override bool Merge(ICommand other) => false;

    public override void Destroy()
    {
    }

    protected override void DoExecute()
    {
        if (_position >= _items.Count)
        {
            throw new Exception("Position out of range");
        }
        
        _item = _items[_position];
        _items.RemoveAt(_position);
    }

    protected override void DoUndo()
    {
        if (_item == null)
        {
            return;
        }
        
        _items.Insert(_position, _item);
    }
}