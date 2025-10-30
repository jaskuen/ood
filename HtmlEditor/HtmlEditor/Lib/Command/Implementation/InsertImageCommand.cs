using HtmlEditor.Extensions;
using HtmlEditor.Lib.Document;
using HtmlEditor.Lib.Document.Img;

namespace HtmlEditor.Lib.Command.Implementation;

public class InsertImageCommand : AbstractCommand
{
    private IList<DocumentItem> _items;
    private readonly int _position;
    private readonly int _width;
    private readonly int _height;
    private readonly string _path;

    private string _copiedImagePath = String.Empty;

    public InsertImageCommand(IList<DocumentItem> items, int position, int width, int height, string path)
    {
        _items = items;
        _position = position;
        _width = width;
        _height = height;
        _path = path;
    }

    public override bool Merge(ICommand other) => false;

    public override void Destroy()
    {
        if (string.IsNullOrWhiteSpace(_copiedImagePath))
        {
            return;
        }

        File.Delete(_copiedImagePath);
    }

    protected override void DoExecute()
    {
        if (string.IsNullOrWhiteSpace(_copiedImagePath))
        {
            string extension = Path.GetExtension(_path);
            _copiedImagePath = Path.Combine(FileExtensions.GetTempFilePath(), Path.ChangeExtension(Path.GetRandomFileName(), extension));

            Directory.CreateDirectory(FileExtensions.GetTempFilePath());

            File.Copy(_path, _copiedImagePath);
        }

        Image image = new Image(_width, _height, _copiedImagePath);
        DocumentItem documentItem = new DocumentItem(image);

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