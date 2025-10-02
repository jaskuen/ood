// See https://aka.ms/new-console-template for more information

using FileEncryptor.Lib.Implementation;

string tempFile = "input.txt";
byte[] buffer = new byte[150];

try
{
    using (var istream = new FileInputStream(tempFile))
    {
        istream.ReadBlock(ref buffer, buffer.Length);
    }

    using (var ostream = new FileOutputStream("output.txt"))
    {
        ostream.WriteBlock(buffer, buffer.Length);
    }
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}