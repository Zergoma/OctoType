using System.Linq.Expressions;

using OctoType.Application.Models;
using OctoType.Domain.Entities;

namespace OctoType.Application.Interfaces;

public interface IDactyloRepository
{
    public Task PersistWordsAsync(
       IReadOnlyCollection<Word> newWords,
       IReadOnlyCollection<Word> updatedWords);

    Task<List<Word>> SearchAsync(WordSearchCriteria criteria);
}
