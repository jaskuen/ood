using System.Text.Json;
using Figures.Model;
using Figures.Model.Core;

namespace Figures.ViewModel.Services.Storage;

/// <summary>
/// Хранилище документов в формате JSON
/// </summary>
public class JsonDocumentStorage : IDocumentStorage
{
    public async Task SaveAsync(string path, DocumentData data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var content = JsonSerializer.Serialize(data, options);
        await File.WriteAllTextAsync(path, content);
    }

    public async Task<DocumentData?> OpenAsync(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            return null;
        }

        try
        {
            var content = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<DocumentData>(content);
        }
        catch
        {
            return null;
        }
    }
}

