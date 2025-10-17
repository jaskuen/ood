using FiguresWithFactory.Lib.Canva;
using FiguresWithFactory.Lib.Canva.Implementation;
using FiguresWithFactory.Lib.ConsoleReaded;
using FiguresWithFactory.Lib.Shapes;

namespace FiguresWithFactory
{
    public static class Program
    {
        public static void Main()
        {
            string outputFileName =
                ConsoleExtensions.ReadStringByDescription(
                    "Enter your future picture file name (without file extension)");

            ConsoleExtensions.WriteFiguresUsage();

            int variant;
            do
            {
                variant = ConsoleExtensions.ReadIntByDescription("Enter 1 for console input, 2 - for file input");
            } while (variant <= 0 || variant > 2);

            ICanvas canvas = new Canvas(1000, 1000);
            Designer designer = new Designer();
            PictureDraft draft;

            if (variant == 1)
            {
                draft = designer.CreateDraft(Console.OpenStandardInput());
            }
            else
            {
                string inputFileName = ConsoleExtensions.ReadStringByDescription("Enter commands file name");
                using FileStream fileStream = new FileStream(inputFileName, FileMode.Open);
                draft = designer.CreateDraft(fileStream);
            }

            Painter painter = new Painter();
            painter.DrawPicture(draft, canvas);

            using StreamWriter streamWriter = new StreamWriter($"{outputFileName}.html");
            streamWriter.Write(canvas.GetDrawing());
            streamWriter.Flush();
        }
    }
}