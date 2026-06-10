using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using OctoType.Domain.Entities;
using OctoType.Domain.Models;
using OctoType.Infrastructure.DbContexts;

using AppInterfaces = OctoType.Application.Interfaces;
using DomainEnities = OctoType.Domain.Entities;

namespace OctoType.Infrastructure.Repositories;

public class DactyloRepository : AppInterfaces.IDactyloRepository
{
    private readonly IDbContextFactory<DactyloDbContext> _factory;
    public DactyloRepository(IDbContextFactory<DactyloDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<Dictionary<string, DomainEnities.Word>> GetWordsByLanguageAsync(string languageCode)
    {
        await using var ctx = await _factory.CreateDbContextAsync();

        return await ctx.Words
            .Include(w => w.Analyses)
            .Where(w => w.LanguageCode == languageCode)
            .ToDictionaryAsync(w => w.Text);
    }

    public async Task PersistWordsAsync(
        IReadOnlyCollection<Word> newWords,
        IReadOnlyCollection<Word> updatedWords)
    {
        await using var ctx = await _factory.CreateDbContextAsync();

        ctx.Words.AddRange(newWords);
        ctx.Words.UpdateRange(updatedWords);

        await ctx.SaveChangesAsync();
    }

    public async Task<List<Word>> GetWordsAsync(
        Expression<Func<Word, bool>> predicate)
    {
        await using var ctx =
            await _factory.CreateDbContextAsync();

        return await ctx.Words
            .Include(w => w.Analyses)
            .Where(predicate)
            .ToListAsync();
    }

    //var ttt =
    //        await _repository.GetWordsAsync(
    //                w => w.Analyses.Any(
    //                    a => (a.FingerMask & (Finger.LeftIndex | Finger.LeftIndex)) != 0
    //                )
    //        );

    public async Task<List<Word>> SearchAsync(WordSearchCriteria criteria)
    {
        await using var ctx = 
            await _factory.CreateDbContextAsync();

        IQueryable<Word> query = ctx.Words
            .AsQueryable();


        bool needsAnalyses =
            criteria.FingerMask.HasValue ||
            criteria.RowMask.HasValue ||
            criteria.Layout.HasValue;

        if (needsAnalyses)
        {
            query = query.Include(w => w.Analyses);
        }

        if (criteria.LanguagesCodes?.Length  > 0 )
        {
            var codes = criteria.LanguagesCodes
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .ToArray();

            if (codes.Length > 0)
            {
                query = query.Where(w => codes.Contains(w.LanguageCode));
            }
        }


        if (criteria.Layout.HasValue)
        {
            query = query.Where(w =>
                w.Analyses.Any(a =>
                    (a.Layout == criteria.Layout)));
        }

        if (criteria.MinLength.HasValue)
        {
            query = query.Where(w => w.Length >= criteria.MinLength.Value);
        }

        if (criteria.MaxLength.HasValue)
        {
            query = query.Where(w => w.Length <= criteria.MaxLength.Value);
        }

        if (criteria.RowMask.HasValue)
        {
            var mask = criteria.RowMask.Value;

            query = query.Where(w =>
                w.Analyses.Any(a =>
                    (a.RowMask & mask) != 0));
        }

        if (criteria.FingerMask.HasValue)
        {
            var mask = criteria.FingerMask.Value;

            query = query.Where(w =>
                w.Analyses.Any(a =>
                    (a.FingerMask & mask) != 0));
        }

        return await query.ToListAsync();
    }
}
