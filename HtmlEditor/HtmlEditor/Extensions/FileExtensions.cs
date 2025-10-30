namespace HtmlEditor.Extensions;

public static class FileExtensions
{
    private static readonly string TempFilePath = GetTemporaryDirectory();

    public static void CopyFilesRecursively(string sourcePath, string targetPath)
    {
        Directory.CreateDirectory(targetPath);

        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
        }

        foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
    }

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