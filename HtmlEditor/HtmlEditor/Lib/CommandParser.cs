using HtmlEditor.Lib.Document;
using HtmlEditor.Lib.Document.Img;
using HtmlEditor.Lib.Document.Paragr;

namespace HtmlEditor.Lib;

public class CommandParser
{
    private const int MaxImageSize = 10000;
    private readonly Menu _menu = new();
    private readonly IDocument _document = new Document.Implementation.Document();

    public CommandParser()
    {
        _menu.AddItem("InsertParagraph",
            "Usage: InsertParagraph <position>|end <text>. Inserts a paragraph into the specified position.",
            InsertParagraph);

        _menu.AddItem("InsertImage",
            "Usage: InsertImage <position>|end <width> <height> <image-path>. Inserts an image with the specified width and height.",
            InsertImage);

        _menu.AddItem("SetTitle",
            "Usage: SetTitle <title>. Sets the document title.",
            SetTitle);

        _menu.AddItem("List",
            "Usage: List. Displays the title and list of document elements.",
            _ => List());

        _menu.AddItem("ReplaceText",
            "Usage: ReplaceText <position> <text>. Replaces the text in the paragraph by the specified position.",
            ReplaceText);

        _menu.AddItem("ResizeImage",
            "Usage: ResizeImage <position> <width> <height>. Resizes the image at the specified position.",
            ResizeImage);

        _menu.AddItem("DeleteItem",
            "Usage: DeleteItem <position>. Deletes an item at the specified position.",
            DeleteItem);

        _menu.AddItem("Help",
            "Usage: Help. Shows the available commands.",
            _ => Help());

        _menu.AddItem("Undo",
            "Usage: Undo. Cancels the last action.",
            _ => Undo());

        _menu.AddItem("Redo",
            "Usage: Redo. Repeats the last canceled action.",
            _ => Redo());

        _menu.AddItem("Save",
            "Usage: Save <path>. Saves the document to a file.",
            Save);

        _menu.AddItem("Exit",
            "Usage: Exit. Exits the program.",
            _ => Exit());
    }

    private void InsertParagraph(StringReader args)
    {
        string? positionInput = ReadWord(args);
        string text = args.ReadToEnd()?.TrimStart() ?? "";

        if (string.IsNullOrWhiteSpace(positionInput) || string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Invalid arguments.");
            return;
        }

        int? position = null;
        if (positionInput != "end")
        {
            if (int.TryParse(positionInput, out int pos))
                position = pos - 1;
            else
            {
                Console.WriteLine("Error: The position must be an integer or 'end'.");
                return;
            }
        }

        _document.InsertParagraph(text, position);
    }

    private void InsertImage(StringReader args)
    {
        string? positionInput = ReadWord(args);
        string? widthStr = ReadWord(args);
        string? heightStr = ReadWord(args);
        string? imagePath = ReadWord(args);

        if (positionInput == null || widthStr == null || heightStr == null || imagePath == null)
        {
            Console.WriteLine("Invalid arguments.");
            return;
        }

        if (!uint.TryParse(widthStr, out uint width) || !uint.TryParse(heightStr, out uint height))
        {
            Console.WriteLine("Error: width and height must be integers.");
            return;
        }

        if (width > MaxImageSize || height > MaxImageSize)
        {
            Console.WriteLine("Size must be between 1 and 10000");
            return;
        }

        int? position = null;
        if (positionInput != "end")
        {
            if (int.TryParse(positionInput, out int pos))
                position = pos - 1;
            else
            {
                Console.WriteLine("Error: The position must be an integer or 'end'.");
                return;
            }
        }

        _document.InsertImage(imagePath, (int)width, (int)height, position);
    }

    private void SetTitle(StringReader args)
    {
        string newTitle = args.ReadToEnd().TrimStart() ?? "";
        _document.SetTitle(newTitle);
    }

    private void List()
    {
        string title = _document.GetTitle();
        int itemCount = _document.GetItemsCount();

        Console.WriteLine($"Title: {title}");
        for (int i = 0; i < itemCount; i++)
        {
            DocumentItem item = _document.GetItem(i);
            IImage? image = item.GetImage();
            IParagraph? paragraph = item.GetParagraph();
            string description;

            if (image != null)
            {
                description = $"Image: {image.GetWidth()} {image.GetHeight()} {image.GetPath()}";
            }
            else if (paragraph != null)
            {
                description = $"Paragraph: {paragraph.GetText()}";
            }
            else
            {
                description = "(unknown)";
            }

            Console.WriteLine($"{i + 1}. {description}");
        }
    }

    private void ReplaceText(StringReader args)
    {
        string? posStr = ReadWord(args);
        string newText = args.ReadToEnd()?.TrimStart() ?? "";

        if (posStr == null || string.IsNullOrEmpty(newText))
        {
            Console.WriteLine("Invalid arguments.");
            return;
        }

        if (!int.TryParse(posStr, out int position))
        {
            Console.WriteLine("Error: invalid position.");
            return;
        }

        _document.ReplaceText(position - 1, newText);
    }

    private void ResizeImage(StringReader args)
    {
        string? posStr = ReadWord(args);
        string? widthStr = ReadWord(args);
        string? heightStr = ReadWord(args);

        if (posStr == null || widthStr == null || heightStr == null)
        {
            Console.WriteLine("Invalid arguments.");
            return;
        }

        if (!int.TryParse(posStr, out int position) ||
            !int.TryParse(widthStr, out int width) ||
            !int.TryParse(heightStr, out int height))
        {
            Console.WriteLine("Invalid arguments.");
            return;
        }

        _document.ResizeImage(position - 1, width, height);
    }

    private void DeleteItem(StringReader args)
    {
        string? posStr = ReadWord(args);
        if (posStr == null || !int.TryParse(posStr, out int position))
        {
            Console.WriteLine("Invalid arguments.");
            return;
        }

        _document.DeleteItem(position - 1);
    }

    private void Help() => _menu.ShowInstructions();

    private void Undo()
    {
        if (!_document.CanUndo())
        {
            Console.WriteLine("Cancellation is not possible!");
            return;
        }

        _document.Undo();
    }

    private void Redo()
    {
        if (!_document.CanRedo())
        {
            Console.WriteLine("Repeat is not possible!");
            return;
        }

        _document.Redo();
    }

    private void Save(StringReader args)
    {
        string? filePath = ReadWord(args);
        if (filePath == null)
        {
            Console.WriteLine("Invalid arguments.");
            return;
        }

        _document.Save(filePath);
    }

    private void Exit()
    {
        _menu.Exit();
    }

    public void Run() => _menu.Run();

    private static string? ReadWord(StringReader reader)
    {
        string word = "";
        int ch;
        while ((ch = reader.Read()) != -1 && !char.IsWhiteSpace((char)ch))
            word += (char)ch;

        // skip spaces
        while (reader.Peek() != -1 && char.IsWhiteSpace((char)reader.Peek()))
            reader.Read();

        return string.IsNullOrEmpty(word) ? null : word;
    }
}