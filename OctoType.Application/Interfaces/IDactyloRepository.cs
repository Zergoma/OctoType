using System.Linq.Expressions;

using OctoType.Domain.Entities;
using OctoType.Domain.Models;

namespace OctoType.Application.Interfaces;

public interface IDactyloRepository
{
    Task<Dictionary<string, Word>> GetWordsByLanguageAsync(string languageCode);

    public Task PersistWordsAsync(
       IReadOnlyCollection<Word> newWords,
       IReadOnlyCollection<Word> updatedWords);


    public Task<List<Word>> GetWordsAsync(
        Expression<Func<Word, bool>> predicate);

    Task<List<Word>> SearchAsync(WordSearchCriteria criteria);
}
