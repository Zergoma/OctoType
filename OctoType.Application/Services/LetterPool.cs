namespace OctoType.Application.Services;

public class LetterPool
{
    static readonly private HashSet<char> VowelsHash = [.. "aeiouy" + "éèêë" + "àâä" + "îï" + "ôö" + "ûü" + "ÿ"];
    static readonly private HashSet<char> ConsonantsHash = [.. "bcdfghjklmnpqrstvwxz" + "ç" + "ß"];

    private readonly Random s_random = Random.Shared;
    private readonly string _vowelsCurrent;
    private readonly string _consonantsCurrent;

    private readonly List<string> _lettersSources = [];
    private bool _sourceSelector;




    static public Result<LetterPool> Create(
        string AllowedChars)
    {
        string vowelsCurrent =
            string.Concat(AllowedChars.Where(VowelsHash.Contains));

        string consonantsCurrent =
            string.Concat(AllowedChars.Where(ConsonantsHash.Contains));

        if (string.IsNullOrEmpty(vowelsCurrent) &&
            string.IsNullOrEmpty(consonantsCurrent))
        {
            return Result<LetterPool>
                .Fail($"Error: No vowels or consonants letter(s)");
        }
        return Result<LetterPool>
            .Ok(new (
                vowelsCurrent,
                consonantsCurrent));
    }

    private LetterPool(
        string Vowels,
        string Consonants)
    {
        _vowelsCurrent = Vowels;
        _consonantsCurrent = Consonants;

        if (!string.IsNullOrWhiteSpace(_vowelsCurrent))
            _lettersSources.Add(_vowelsCurrent);

        if (!string.IsNullOrWhiteSpace(_consonantsCurrent))
            _lettersSources.Add(_consonantsCurrent);

        _sourceSelector = s_random.Next(2) == 0;       
    }
    private string GetSource()
    {
        if (_lettersSources.Count == 1)
        {
            return _lettersSources[0];
        }
        return _lettersSources[_sourceSelector ? 0 : 1];
    }

    public char GetLetter()
    {
        string source = GetSource();
        char charSelected = source[s_random.Next(source.Length)];
        _sourceSelector = !_sourceSelector;

        return charSelected;
    }
}