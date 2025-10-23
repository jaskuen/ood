using HtmlEditor.Lib.Document.Img;
using HtmlEditor.Lib.Document.Paragr;

namespace HtmlEditor.Lib.Document;

public class DocumentItem : ConstDocumentItem
{
    private readonly Image? _image = null;
    private readonly Paragraph? _paragraph = null;
    
    public DocumentItem(Image image) : base(new ConstImage(image))
    {
        _image = image;
    }

    public DocumentItem(Paragraph paragraph) : base(new ConstParagraph(paragraph))
    {
        _paragraph = paragraph;
    }

    public override IImage? GetImage()
    {
        return _image;
    }

    public override IParagraph? GetParagraph()
    {
        return _paragraph;
    }
}