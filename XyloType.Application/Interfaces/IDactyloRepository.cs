using System.Linq.Expressions;

using XyloType.Application.Models;
using XyloType.Domain.Entities;

namespace XyloType.Application.Interfaces;

public interface IDactyloRepository
{
    public Task PersistWordsAsync(
       IReadOnlyCollection<Word> newWords,
       IReadOnlyCollection<Word> updatedWords);

    Task<List<Word>> SearchAsync(WordSearchCriteria criteria);
}
