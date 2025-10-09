// See https://aka.ms/new-console-template for more information

using FileEncryptor.Lib;
using FileEncryptor.Lib.Crypt;
using FileEncryptor.Lib.Decorators;
using FileEncryptor.Lib.Implementation;
using FileEncryptor.Lib.Parser;

public class Program
{
    public static void Main(string[] args)
    {
        Commands commands = CommandParser.ParseCommandLineArgs(args);
        
        IList<byte> data = new List<byte>();

        using (IInputStream inputStream = new FileInputStream(commands.InputFileName))
        {
            IInputStream copy = inputStream;

            foreach (var command in commands.InputCommands!)
            {
                switch (command.Key)
                {
                    case InputCommand.Decrypt:
                        copy = new Decryptor(copy, command.Value);
                        break;
                    case InputCommand.Decompress:
                        copy = new Decompressor(copy);
                        break;
                }
            }
            copy.ReadBlock(data, 1024);
        }

        using (IOutputStream outputStream = new FileOutputStream(commands.OutputFileName))
        {
            IOutputStream copy = outputStream;

            foreach (var command in commands.OutputCommands)
            {
                switch (command.Key)
                {
                    case OutputCommand.Encrypt:
                        copy = new Encryptor(copy, command.Value);
                        break;
                    case OutputCommand.Compress:
                        copy = new Compressor(copy);
                        break;
                }
            }
            copy.WriteBlock(data, 1024);
        }
    }
}