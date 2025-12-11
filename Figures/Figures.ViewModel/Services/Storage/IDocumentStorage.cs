using Figures.Model;
using Figures.Model.Core;

namespace Figures.ViewModel.Services.Storage;

/// <summary>
/// Интерфейс для сохранения/открытия документов
/// </summary>
public interface IDocumentStorage
{
    Task SaveAsync(string path, DocumentData data);
    Task<DocumentData?> OpenAsync(string path);
}

