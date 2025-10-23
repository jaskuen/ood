using HtmlEditor.Lib.Document.Img;
using HtmlEditor.Lib.Document.Paragr;

namespace HtmlEditor.Lib.Document;

public class ConstDocumentItem
{
    private readonly ConstImage? _image = null;
    private readonly ConstParagraph? _paragraph = null;
    
    public ConstDocumentItem(ConstImage image)
    {
        _image = image;
    }

    public ConstDocumentItem(ConstParagraph paragraph)
    {
        _paragraph = paragraph;
    }

    public virtual IImage? GetImage()
    {
        return _image;
    }

    public virtual IParagraph? GetParagraph()
    {
        return _paragraph;
    }
}