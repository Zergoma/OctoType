using OctoType.Application.Interfaces;
using OctoType.Application.ValueObjects;

namespace OctoType.Application.Services;

public class PseudoWordBatchGenerator : IPseudoWordBatchGenerator
{
    private readonly IPseudoWordGeneratorService _wordGenerator;

    public PseudoWordBatchGenerator(
        IPseudoWordGeneratorService wordGenerator)
    {
        _wordGenerator = wordGenerator;
    }

    public Result<List<string>> Generate(int count, PseudoWordOptions options)
    {
        if (count <= 0)
            return Result<List<string>>.Fail("Count must be > 0");

        List<string> words = new(count);

        foreach (int _ in Enumerable.Range(0, count))
        {
            var result = _wordGenerator.Generate(options);

            if (!result.Success)
                return Result<List<string>>.Fail(result.Error);

            words.Add(result.Value!);
        }

        return Result<List<string>>.Ok(words);
    }
}
