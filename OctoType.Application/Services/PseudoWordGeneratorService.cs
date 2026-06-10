using System.Text;

using Microsoft.Extensions.Options;

using OctoType.Application.Interfaces;
using OctoType.Application.ValueObjects;

namespace OctoType.Application.Services;

public class PseudoWordGeneratorService : IPseudoWordGeneratorService
{
    private static readonly Random s_random = Random.Shared;
    private const string _vowels = "aàeéêèiîouûy";
    private const string _consonants = "bcdfghjklmnpqrstvwxz";
    
    Result<LetterPool>? _letterPoolResu = null;
    PseudoWordOptions? _LetterOption = null;

    static private int GetLength(int val1, int val2)
    {
        return val1 == val2
            ? val1
            : s_random.Next(
                Math.Min(val1, val2), 
                Math.Max(val1, val2) + 1);
    }

    public Result<string> Generate(PseudoWordOptions options)
    {
        // first time
        // get the option an build the letterPool
        if(_LetterOption is null)
        {
            _LetterOption = options;
            _letterPoolResu =
                LetterPool.Create(_vowels, _consonants, options.AllowedChars);
        }
        // next time
        // compare option if delta, re-create the letterPool
        else if(_LetterOption != options)
        {
            _letterPoolResu =
                LetterPool.Create(_vowels, _consonants, options.AllowedChars);
        }

        // does the letterPool operational ?
        if (!_letterPoolResu!.Success)
            return Result<string>
                .Fail(_letterPoolResu.Error);

        LetterPool letterSource = _letterPoolResu.Value!;

        //int length = GetLength();
        int length = 
            GetLength(options.MinLength, options.MaxLength);

        StringBuilder result = new(length);
      
        foreach( var _ in Enumerable.Range(0, length))
        {
            result.Append(letterSource.GetLetter());
        }

        return Result<string>
            .Ok(result.ToString());
    }
}
