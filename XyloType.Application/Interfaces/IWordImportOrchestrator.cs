namespace XyloType.Application.Interfaces;

public interface IWordImportOrchestrator
{
    Task<Result<bool>> ImportAsync(string filePath, string languageCode, IKeyboardKeysLocator layout);
}