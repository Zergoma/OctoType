namespace OctoType.Application.Interfaces;

public interface IStringsProvider
{
    Task<Result<IEnumerable<string>>> GetStringsAsync();
}
