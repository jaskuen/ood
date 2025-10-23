namespace HtmlEditor.Lib.Document.Paragr;

public class Paragraph : IParagraph
{
    private string _text;

    public Paragraph(string text)
    {
        _text = text;
    }

    public string GetText()
    {
        return _text;
    }

    public void SetText(string text)
    {
        _text = text;
    }
}