using XyloType.Application;

namespace XyloType.Application.Interfaces;

public interface IStringsProvider
{
    Task<Result<IEnumerable<string>>> GetStringsAsync();
}
