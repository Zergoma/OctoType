using System.Text;

using XyloType.Application;
using XyloType.Application.Interfaces;
using XyloType.Application.Models;
using XyloType.Application.ValueObjects;

namespace XyloType.Application.Services;

public class PseudoWordGeneratorService : IPseudoWordGeneratorService
{
    private readonly IGetNextInRange _getNext;

    public PseudoWordGeneratorService(IGetNextInRange getNext)
    {
        _getNext = getNext;
    }
    
    Result<LetterPool>? _letterPoolResu = null;
    PseudoWordOptions? _LetterOption = null;

    private int GetLength(int val1, int val2)
    {
        return val1 == val2
            ? val1
            : _getNext.GetNext(
                Math.Min(val1, val2), 
                Math.Max(val1, val2) + 1);
    }

    public Result<string> Generate(PseudoWordOptions options)
    {
        // first time or option delta
        // get the option and build the letterPool
        if(_LetterOption is null || _LetterOption != options)
        {
            _LetterOption = options;
            _letterPoolResu =
                LetterPool.Create(options.AllowedChars);
        }
        
        // does the letterPool operational ?
        if (!_letterPoolResu!.Success)
            return Result<string>
                .Fail(_letterPoolResu.Error);

        LetterPool letterSource = _letterPoolResu.GetValue;

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
