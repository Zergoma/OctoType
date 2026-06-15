using OctoType.Application.ValueObjects;
using OctoType.Domain.Entities;

namespace OctoType.Application.Interfaces
{
    public interface IWordBatchProcessorOrchestrator
    {
        Result<WordProcessResult> Process(
            IReadOnlyDictionary<string, int> batch,
            IReadOnlyDictionary<string, Word> existingWords,
            string languageCode,
            IKeyboardKeysLocator layout);
    }
}