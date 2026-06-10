namespace OctoType.Application.Services;

public class LetterPool
{
    private readonly Random s_random = Random.Shared;
    private readonly string _vowelsCurrent;
    private readonly string _consonantsCurrent;

    private readonly List<string> _lettersSources = [];
    private bool _sourceSelector;


    /// <summary>
    /// Vowel-consonant alternation Letter Generator
    /// 
    /// To be generated, letters have be contains in AllowedCharts 
    /// if no vowels: only consonants
    /// if no consonants : only vowels
    /// 
    /// </summary>
    /// <param name="Vowels">_vowels list</param>
    /// <param name="Consonants">_consonants list</param>
    /// <param name="AllowedChars">Only letters that we want</param>
    /// <returns></returns>
    static public Result<LetterPool> Create(
        string Vowels,
        string Consonants,
        string AllowedChars)
    {
        string vowelsCurrent =
            string.Concat(Vowels.Where(c => AllowedChars.Contains(c)));

        string consonantsCurrent =
            string.Concat(Consonants.Where(c => AllowedChars.Contains(c)));

        if (string.IsNullOrEmpty(vowelsCurrent) &&
            string.IsNullOrEmpty(consonantsCurrent))
        {
            return Result<LetterPool>
                .Fail($"Error: No letter: vowels: {vowelsCurrent}, consonants: {consonantsCurrent}, allowed letters: {AllowedChars}");
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