namespace HtmlEditor.Lib;

public class Menu
{
    public delegate void Command(StringReader args);

    private readonly List<Item> _items = new();
    private bool _exit = false;

    public void AddItem(string shortcut, string description, Command command)
    {
        _items.Add(new Item(shortcut, description, command));
    }

    public void Run()
    {
        ShowInstructions();

        while (!_exit)
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            if (input == null)
                break;

            try
            {
                ExecuteCommand(input);
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public void ShowInstructions()
    {
        Console.WriteLine("Commands:");
        foreach (var item in _items)
        {
            Console.WriteLine($"\t{item.Shortcut}. {item.Description}");
        }
    }

    public void Exit()
    {
        _exit = true;
    }

    private void ExecuteCommand(string commandLine)
    {
        using StringReader reader = new StringReader(commandLine);
        string? name = ReadNextWord(reader);

        Item? item = _items.FirstOrDefault(i => string.Equals(i.Shortcut, (name ?? ""), StringComparison.CurrentCultureIgnoreCase));
        if (item == null)
        {
            throw new ArgumentException($"Unknown command {name}");
        }

        item.Command(reader);
    }

    private static string? ReadNextWord(StringReader reader)
    {
        string word = "";
        int ch;
        while ((ch = reader.Read()) != -1 && !char.IsWhiteSpace((char)ch))
        {
            word += (char)ch;
        }

        while ((ch = reader.Peek()) != -1 && char.IsWhiteSpace((char)ch))
        {
            reader.Read();
        }

        return string.IsNullOrWhiteSpace(word) ? null : word;
    }

    private class Item
    {
        public Item(string shortcut, string description, Command command)
        {
            Shortcut = shortcut;
            Description = description;
            Command = command;
        }

        public string Shortcut { get; }
        public string Description { get; }
        public Command Command { get; }
    }
}