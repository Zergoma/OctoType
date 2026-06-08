namespace OctoType.Application.Interfaces;

public interface IWordStreamReader
{
    public event Action<string>? LineChanged;
    IAsyncEnumerable<string> ReadWordsAsync(string filePath);
}
