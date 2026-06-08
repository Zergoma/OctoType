using OctoType.Domain.Enums;

namespace OctoType.Application.Interfaces
{
    public interface IWordImportServiceOrchestrator
    {
        Task ImportAsync(string filePath, string languageCode, KeyboardLayout layout);
    }
}