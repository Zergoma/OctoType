using OctoType.Application.Interfaces;

namespace OctoType.Application.Models;

public class StaticStringProvider : IStringsProvider
{
    private readonly List<string> _lines;
    public StaticStringProvider(List<string> lines)
    {
        _lines = lines;
    }
    public Task<IEnumerable<string>> GetStringsAsync()
        => Task.FromResult<IEnumerable<string>>(_lines);
}
