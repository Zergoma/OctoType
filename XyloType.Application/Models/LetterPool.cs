using XyloType.Application.Validators;
using XyloType.Application.ValueObjects;

namespace XyloType.Application.Models;

public class LetterPool
{
    private readonly Random s_random = Random.Shared;

    static readonly public HashSet<char> VowelsHash = [.. "aeiouy" + "éèêë" + "àâä" + "îï" + "ôö" + "ûü" + "ÿ"
                                                         + "AEIOUY" + "ÉÈÊË" + "ÀÂÄ"+ "ÎÏ" + "ÔÖ" + "ÛÜ"];
    static readonly public HashSet<char> ConsonantsHash = [.. "bcdfghjklmnpqrstvwxz" + "ç" + "ß"
                                                             + "BCDFGHJKLMNPQRSTVWXZ" + "Ç" ];

    
    /// <summary>
    /// contains string, each represent vowels, or consonants
    /// </summary>
    private readonly IList<string> _lettersSources = [];

    private bool _sourceSelector;


    static public FilteredCharTypes FilterToType(string selectedChars)
    {
        string vowelsCurrent =
            string.Concat(selectedChars.Where(VowelsHash.Contains));

        string consonantsCurrent =
            string.Concat(selectedChars.Where(ConsonantsHash.Contains));

        return new(vowelsCurrent, consonantsCurrent);
    }


    /// <summary>
    /// From a raw source, it will separate vowels and Consonants and Build an instance of LetterPool
    /// </summary>
    /// <param name="AllowedChars"></param>
    /// <returns>Ok if vowels or consonant have at least 1 element else Fail</returns>
    static public Result<LetterPool> Create(
        string AllowedChars)
    {
        FilteredCharTypes filterToType = FilterToType(AllowedChars);

        FilteredCharTypesValidator validator = new();
        var resuValidator = validator.Validate(filterToType);
        if (!resuValidator.IsValid)
        {
            return Result<LetterPool>
                .Fail($"Error: No vowels or consonants letter(s)");
        }

        return Result<LetterPool>
            .Ok(new (
                filterToType.Vowels,
                filterToType.Consonants));
    }

    private LetterPool(
        string vowels,
        string consonants)
    {
        if (!string.IsNullOrWhiteSpace(vowels))
            _lettersSources.Add(vowels);

        if (!string.IsNullOrWhiteSpace(consonants))
            _lettersSources.Add(consonants);

        _sourceSelector = s_random.Next(2) == 0;       
    }

    private string GetSource()
    {
        // only one -> get it
        if (_lettersSources.Count == 1)
        {
            return _lettersSources[0];
        }
        
        // more than one, get current selected one
        return _lettersSources[_sourceSelector ? 0 : 1];
    }

    /// <summary>
    /// Get a random Letter from the pool
    /// </summary>
    /// <returns></returns>
    public char GetLetter()
    {
        string source = GetSource();
        char charSelected = source[s_random.Next(source.Length)];
        
        // switch between vowels and consonant to have somthing more readable
        _sourceSelector = !_sourceSelector;

        return charSelected;
    }
}