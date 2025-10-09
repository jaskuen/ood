namespace FileEncryptor.Lib.Parser;

public enum InputCommand
{
    Decrypt,
    Decompress
}

public enum OutputCommand
{
    Encrypt,
    Compress
}

public struct Commands
{
    public IList<KeyValuePair<InputCommand, int>> InputCommands;
    public IList<KeyValuePair<OutputCommand, int>> OutputCommands;
    public string InputFileName;
    public string OutputFileName;

    public Commands()
    {
        InputCommands = new List<KeyValuePair<InputCommand, int>>();
        OutputCommands = new List<KeyValuePair<OutputCommand, int>>();
    }
}

public static class CommandParser
{
    public static Commands ParseCommandLineArgs(string[] args)
    {
        Commands commands = new Commands();
        IList<KeyValuePair<InputCommand, int>> decryptCommands = [];
        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];

            if (i == args.Length - 2)
            {
                commands.InputFileName = arg;
                continue;
            }

            if (i == args.Length - 1)
            {
                commands.OutputFileName = arg;
                continue;
            }

            switch (arg.ToLower())
            {
                case "--encrypt":
                    if (!int.TryParse(args[++i], out int encryptKey))
                    {
                        throw new ArgumentException($"Failed to parse encryption key: {args[i + 1]}");
                    }

                    commands.OutputCommands?.Add(
                        new KeyValuePair<OutputCommand, int>(OutputCommand.Encrypt, encryptKey));
                    break;
                case "--decrypt":
                    if (!int.TryParse(args[++i], out int decryptKey))
                    {
                        throw new ArgumentException($"Failed to parse decryption key: {args[i + 1]}");
                    }

                    commands.InputCommands?.Add(new KeyValuePair<InputCommand, int>(InputCommand.Decrypt, decryptKey));
                    // decryptCommands.Add(new KeyValuePair<InputCommand, int>(InputCommand.Decrypt, decryptKey));
                    break;
                case "--compress":
                    commands.OutputCommands?.Add(new KeyValuePair<OutputCommand, int>(OutputCommand.Compress, 0));
                    break;
                case "--decompress":
                    // foreach (var keyValue in decryptCommands.Reverse())
                    // {
                    //     commands.InputCommands?.Add(keyValue);
                    // }
                    //
                    // decryptCommands = [];
                    commands.InputCommands?.Add(new KeyValuePair<InputCommand, int>(InputCommand.Decompress, 0));
                    break;
                default:
                    throw new ArgumentException($"Unknown command: {arg}");
            }
        }

        foreach (var keyValue in decryptCommands.Reverse())
        {
            commands.InputCommands?.Add(keyValue);
        }

        commands.InputCommands = commands.InputCommands?.Reverse().ToList();

        return commands;
    }
}