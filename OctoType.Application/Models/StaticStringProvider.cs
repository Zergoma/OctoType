using OctoType.Application.Interfaces;

namespace OctoType.Application.Models;

public class StaticStringProvider : IStringsProvider
{
    private readonly List<string> _lines;
    public StaticStringProvider(List<string> lines)
    {
        _lines = lines;
    }
    public async Task<Result<IEnumerable<string>>> GetStringsAsync()
        => Result<IEnumerable<string>>.Ok(_lines);

}
