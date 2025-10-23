namespace HtmlEditor.Lib.Document.Paragr;

public class ConstParagraph : IParagraph
{
    private IParagraph _paragraph;
    
    public ConstParagraph(IParagraph paragraph)
    {
        _paragraph = paragraph;
    }

    public string GetText() => _paragraph.GetText();

    public void SetText(string text)
    {
    }
}