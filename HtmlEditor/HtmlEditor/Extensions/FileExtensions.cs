using System.Security.Cryptography;

namespace HtmlEditor.Extensions;

public static class FileExtensions
{
    private static readonly string TempFilePath = GetTemporaryDirectory();

    public static void DeleteTempImagesFolder()
    {
        Directory.Delete(TempFilePath, true);
    }

    public static string GetTempFilePath() => TempFilePath;

    private static string GetTemporaryDirectory()
    {
        string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        if (File.Exists(tempDirectory))
        {
            return GetTemporaryDirectory();
        }

        Directory.CreateDirectory(tempDirectory);
        return tempDirectory;
    }
}