using System.Text.RegularExpressions;

using OctoType.Application.Interfaces;

namespace OctoType.Infrastructure.IO;

public sealed class TextFileWordReader : IWordStreamReader
{
    public event Action<string>? LineChanged;

    private static readonly Regex WordRegex =
        new(@"\p{L}+", RegexOptions.Compiled);

    public async IAsyncEnumerable<string> ReadWordsAsync(string filePath)
    {
        using StreamReader reader = new(filePath);

        string? line;

        while ((line = await reader.ReadLineAsync()) is not null)
        {
            LineChanged?.Invoke(line);
            foreach (Match match in WordRegex.Matches(line))
            {
                yield return match.Value.ToLowerInvariant();
            }

            await Task.Yield(); // optionnel mais propre pour gros fichiers
        }
    }
}



public sealed class LineReader
{
    private readonly string _filePath;
    public LineReader(string filePath)
    {
        this._filePath = filePath;
    }

    public async IAsyncEnumerable<string> ReadLineAsync()
    {
        using StreamReader reader = new(_filePath);

        string? line;

        while ((line = await reader.ReadLineAsync()) is not null)
        {
            yield return line;

            await Task.Yield(); // optionnel mais propre pour gros fichiers
        }
    }
}