namespace OctoType.Application.Interfaces;

public interface IStringsProviderService
{
    Task<IEnumerable<string>> GetStringsAsync();
}
