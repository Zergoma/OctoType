using Microsoft.EntityFrameworkCore;

using OctoType.Domain.Entities;
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
}

