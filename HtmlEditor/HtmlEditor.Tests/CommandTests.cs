using HtmlEditor.Lib.Command;
using HtmlEditor.Lib.Command.Implementation;
using HtmlEditor.Lib.Document;
using HtmlEditor.Lib.Document.Img;
using HtmlEditor.Lib.Document.Paragr;

namespace HtmlEditor.Tests;

public class CommandTests
{
    private IList<DocumentItem> _items;

    [SetUp]
    public void Setup()
    {
        _items = new List<DocumentItem>();
    }

    [Test]
    public void TestInsertParagraphCommand()
    {
        IList<DocumentItem> expected =
        [
            new(new Paragraph("2")),
            new(new Paragraph("0")),
            new(new Paragraph("1")),
        ];

        ICommand command = new InsertParagraphCommand(_items, 0, "0");
        command.Execute();
        command = new InsertParagraphCommand(_items, 1, "1");
        command.Execute();
        command = new InsertParagraphCommand(_items, 0, "2");
        command.Execute();

        Assert.That(
            _items.Select(x => x.GetParagraph()!.GetText()),
            Is.EquivalentTo(expected.Select(x => x.GetParagraph()!.GetText())));

        command.Undo();
        expected = expected.Where(x => x.GetParagraph()!.GetText() != "2").ToList();

        Assert.That(
            _items.Select(x => x.GetParagraph()!.GetText()),
            Is.EquivalentTo(expected.Select(x => x.GetParagraph()!.GetText())));
    }

    [Test]
    public void TestInsertImageCommand()
    {
        IList<DocumentItem> expected =
        [
            new(new Image(1, 1, "1.png")),
            new(new Image(0, 0, "0.png")),
            new(new Image(2, 2, "2.png")),
        ];

        using (File.Create("0.png")) ;
        using (File.Create("1.png")) ;
        using (File.Create("2.png")) ;

        ICommand command = new InsertImageCommand(_items, 0, 0, 0, "0.png");
        command.Execute();
        command = new InsertImageCommand(_items, 1, 1, 1, "1.png");
        command.Execute();
        command = new InsertImageCommand(_items, 0, 2, 2, "2.png");
        command.Execute();

        Assert.That(
            _items.Select(x => x.GetImage()!.GetWidth()),
            Is.EquivalentTo(expected.Select(x => x.GetImage()!.GetWidth())));

        command.Undo();
        expected = expected.Where(x => x.GetImage()!.GetWidth() != 2).ToList();

        Assert.That(
            _items.Select(x => x.GetImage()!.GetWidth()),
            Is.EquivalentTo(expected.Select(x => x.GetImage()!.GetWidth())));
    }

    [Test]
    public void TestReplaceTextCommand()
    {
        IList<DocumentItem> expected =
        [
            new(new Paragraph("2")),
            new(new Paragraph("0")),
        ];

        _items = expected;
    }
}