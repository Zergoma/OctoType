using OctoType.Application.DTOs;

namespace OctoType.Application.Interfaces
{
    public interface IWordImportOrchestrator
    {
        Task<Result<bool>> ImportAsync(string filePath, string languageCode, IKeyboardKeysLocator layout);
    }
}