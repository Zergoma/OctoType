using OctoType.Application.ValueObjects;
using OctoType.Domain.Entities;
using OctoType.Domain.Enums;

namespace OctoType.Application.Interfaces
{
    public interface IWordBatchProcessorService
    {
        WordProcessResult Process(IReadOnlyDictionary<string, int> batch, IReadOnlyDictionary<string, Word> existingWords, string languageCode, KeyboardLayout layout);
    }
}