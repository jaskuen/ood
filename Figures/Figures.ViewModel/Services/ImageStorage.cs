using Figures.Model;
using Figures.Model.Core;

namespace Figures.ViewModel.Services;

/// <summary>
/// Менеджер для хранения и управления изображениями
/// </summary>
public class ImageStorage
{
    private class StoredImage
    {
        public byte[] Data { get; init; } = [];
        public int RefCount { get; set; }
        public string? Name { get; init; }
    }

    private readonly Dictionary<string, StoredImage> _images = new();

    public Task<(string id, byte[] data)> StoreFileAsync(byte[] fileData, string? name = null)
    {
        var id = Guid.NewGuid().ToString();
        return StoreDataAsync(id, fileData, name);
    }

    public Task<(string id, byte[] data)> StoreDataAsync(string id, byte[] data, string? name = null)
    {
        if (_images.ContainsKey(id))
        {
            _images[id].RefCount += 1;
            return Task.FromResult((id, _images[id].Data));
        }

        _images[id] = new StoredImage
        {
            Data = data,
            RefCount = 0,
            Name = name
        };
        return Task.FromResult((id, data));
    }

    public void Retain(string id)
    {
        if (_images.TryGetValue(id, out var image))
        {
            image.RefCount += 1;
        }
    }

    public void Release(string id)
    {
        if (!_images.TryGetValue(id, out var image)) return;
        image.RefCount -= 1;
        if (_images[id].RefCount <= 0)
        {
            _images.Remove(id);
        }
    }

    public byte[]? GetImageData(string id)
    {
        return _images.TryGetValue(id, out var image) ? image.Data : null;
    }

    public Dictionary<string, ImageData> Export(string[] imageIds)
    {
        var result = new Dictionary<string, ImageData>();
        foreach (var id in imageIds)
        {
            if (_images.TryGetValue(id, out var stored))
            {
                var base64 = Convert.ToBase64String(stored.Data);
                result[id] = new ImageData
                {
                    DataUrl = $"data:image/png;base64,{base64}",
                    Name = stored.Name
                };
            }
        }

        return result;
    }

    public void Dispose()
    {
        _images.Clear();
    }
}

