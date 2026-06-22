namespace OctoType.Application.Interfaces;

public interface IStringsProvider
{
    Task<IEnumerable<string>> GetStringsAsync();
}
