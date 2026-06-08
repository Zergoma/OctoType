using OctoType.Domain.Entities;

namespace OctoType.Application.Interfaces;

public interface IDactyloRepository
{
    Task<Dictionary<string, Word>> GetWordsByLanguageAsync(string languageCode);

    public Task PersistWordsAsync(
       IReadOnlyCollection<Word> newWords,
       IReadOnlyCollection<Word> updatedWords);
}
