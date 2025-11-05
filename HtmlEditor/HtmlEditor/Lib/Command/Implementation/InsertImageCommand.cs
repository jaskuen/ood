using HtmlEditor.Extensions;
using HtmlEditor.Lib.Document;
using HtmlEditor.Lib.Document.Img;

namespace HtmlEditor.Lib.Command.Implementation;

public class InsertImageCommand : AbstractCommand
{
    private IDocument _document;
    private readonly int? _position;
    private readonly int _width;
    private readonly int _height;
    private readonly string _path;

    private string _copiedImagePath = String.Empty;

    public InsertImageCommand(IDocument document, int? position, int width, int height,
        string path)
    {
        _document = document;
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
        _document.InsertImage(_path, _width, _height, _position);
    }

    protected override void DoUndo()
    {
        if (_document.GetItemsCount() > _position)
        {
            _document.DeleteItem(_position.Value);
        }
    }
}