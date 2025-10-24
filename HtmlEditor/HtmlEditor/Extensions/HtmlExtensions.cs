using System.Text;
using HtmlEditor.Lib.Document;
using HtmlEditor.Lib.Document.Img;
using HtmlEditor.Lib.Document.Implementation;
using HtmlEditor.Lib.Document.Paragr;

namespace HtmlEditor.Extensions;

public static class HtmlExtensions
{
    public static void CreateHtmlDocument(Document document, string filePath)
    {
        string? directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using var writer = new StreamWriter(filePath, false, Encoding.UTF8);

        writer.WriteLine("<!DOCTYPE html>");
        writer.WriteLine("<html lang=\"en\">");
        writer.WriteLine("<head>");
        writer.WriteLine($"<title>{HtmlEncode(document.GetTitle())}</title>");
        writer.WriteLine("</head>");
        writer.WriteLine("<body>");


        for (int i = 0; i < document.GetItemsCount(); i++)
        {
            ConstDocumentItem item = document.GetItem(i);

            IParagraph? paragraph = item.GetParagraph();
            IImage? image = item.GetImage();

            // Копировать только необходимые на момент
            // сохранения изображения

            if (paragraph != null)
            {
                writer.WriteLine($"<p>{HtmlEncode(paragraph.GetText())}</p>");
            }

            if (image != null)
            {
                var imageName = Path.GetFileName(image.GetPath());
                var imagePath = $"images/{imageName}";

                writer.WriteLine(
                    $"<img src=\"{HtmlEncode(imagePath)}\" width=\"{image.GetWidth()}\" height=\"{image.GetHeight()}\" />");

                if (!Directory.Exists(Path.Combine([directory ?? "", "images"])))
                {
                    Directory.CreateDirectory(Path.Combine([directory ?? "", "images"]));
                }

                File.Copy($"tempImages/{imageName}", Path.Combine([directory ?? "", "images", imageName]), true);
            }
        }

        writer.WriteLine("</body>");
        writer.WriteLine("</html>");
    }

    private static string HtmlEncode(string value)
    {
        return value
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&apos;");
    }
}