// See https://aka.ms/new-console-template for more information

using DrawFigures.CommandExecutor;
using DrawFigures.CommandExecutor.Implementation;
using DrawFigures.Drawer;
using DrawFigures.Drawer.Implementation;
using DrawFigures.Picture;
using DrawFigures.Picture.Implementation;

string? fileName = null;
while (fileName == null)
{
    Console.Write("Enter your picture file name: ");
    fileName = Console.ReadLine();
}

IDrawer drawer = new FileDrawer(fileName);
IPicture picture = new Picture(800, 600);
ICommandExecutor commandExecutor = new CommandExecutor(picture);


string? command = Console.ReadLine();
while (command != null)
{
    try
    {
        if (commandExecutor.ExecuteCommand(command))
        {
            break;
        }
        command = Console.ReadLine();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}

drawer.Draw(picture.GetBitmap());