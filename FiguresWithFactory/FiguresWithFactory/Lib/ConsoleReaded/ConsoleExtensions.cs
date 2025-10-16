namespace FiguresWithFactory.Lib.ConsoleReaded;

public static class ConsoleExtensions
{
    public static string ReadStringByDescription(string description)
    {
        string? value = null;
        while (string.IsNullOrWhiteSpace(value))
        {
            Console.Write(description + ": ");
            value = Console.ReadLine();
        }

        return value;
    }

    public static int ReadIntByDescription(string description)
    {
        int value = 0;

        do
        {
            Console.Write(description + ": ");
        } while (!int.TryParse(Console.ReadLine(), out value));

        return value;
    }

    public static void WriteFiguresUsage()
    {
        Console.WriteLine("""
                          Figures commands:
                          rectangle <color> <x1> <y1> <x2> <y2>
                          triangle <color> <x1> <y1> <x2> <y2> <x3> <y3>
                          ellipse <color> <cx> <cy> <rx> <ry>
                          regular <color> <cx> <cy> <vertex count> <radius>
                          exit - exit program
                          """
        );
    }
}