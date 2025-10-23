using HtmlEditor.Lib.Document;
using HtmlEditor.Lib.Document.Img;

namespace HtmlEditor.Lib.Command.Implementation;

public class ResizeImageCommand : AbstractCommand
{
    private IList<DocumentItem> _items;
    private readonly int _position;
    private int _oldWidth;
    private int _oldHeight;
    private int _newWidth;
    private int _newHeight;

    public ResizeImageCommand(IList<DocumentItem> items, int position, int newWidth, int newHeight)
    {
        _items = items;
        _position = position;
        _newWidth = newWidth;
        _newHeight = newHeight;
    }

    public override bool Merge(ICommand other)
    {
        if (other is ResizeImageCommand otherCommand)
        {
            DocumentItem item = _items[_position];
            IImage? image = item.GetImage();

            if (image == null)
            {
                throw new Exception("Item is not an image");
            }

            _newWidth = otherCommand._newWidth;
            _newHeight = otherCommand._newHeight;
            image.Resize(_newWidth, _newHeight);

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

        IImage? image = _items[_position].GetImage();

        if (image == null)
        {
            throw new Exception("Item is not an image");
        }

        _oldWidth = image.GetWidth();
        _oldHeight = image.GetHeight();
        image.Resize(_newWidth, _newHeight);
    }

    protected override void DoUndo()
    {
        if (_position >= _items.Count)
        {
            throw new Exception("Position out of range");
        }

        IImage? image = _items[_position].GetImage();

        if (image == null)
        {
            throw new Exception("Item is not an image");
        }

        _newWidth = image.GetWidth();
        _newHeight = image.GetHeight();
        image.Resize(_oldWidth, _oldHeight);
    }
}