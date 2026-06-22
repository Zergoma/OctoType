using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.Models.Typing.Engine;

public class TypingExercicesEngine
{
    public TypingExercices Exercice { get; set; }
    private int _idx = 0;
    public TypingExercicesEngine(TypingExercices exercice, int idx)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(idx, nameof(idx));
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(idx, exercice.Exercices.Count, nameof(idx));

        Exercice = exercice;
        _idx = idx;
    }

    public Result<TypingExercise> NextExercice()
    {
        int nextIdx = _idx + 1;
        if (nextIdx >= Exercice.Exercices.Count)
            return Result<TypingExercise>.Fail("No more item");

        _idx = nextIdx;
        return Result<TypingExercise>
            .Ok(Exercice.Exercices[_idx]);
    }


    public Result<TypingExercise> PreviousExercice()
    {
        int previousIdx = _idx - 1;
        if (previousIdx < 0)
            return Result<TypingExercise>.Fail("No more item");

        _idx = previousIdx;
        return Result<TypingExercise>
            .Ok(Exercice.Exercices[_idx]);
    }

    public Result<TypingExercise> CurrentExercice()
    {
        if (_idx < 0 || _idx >= Exercice.Exercices.Count)
            return Result<TypingExercise>.Fail($"No item at {_idx}");

        return Result<TypingExercise>
            .Ok(Exercice.Exercices[_idx]);
    }
}


public static class CreateStringProvider
{
    public static Result<IStringsProvider> Create(
        TypingExercise exercice,
        KeyBoardLayoutDto selectedKeyboard)
    {
        if (exercice.Dynamic is not null)
        {
            // crete dynamic one
        }

        if (exercice.Static is not null)
        {
            // create static one from generatde data inside the exercice
            return Result<IStringsProvider>
                .Ok(new TypingExerciceStaticData(exercice, selectedKeyboard));

        }
    }
}

public class TypingExerciceStaticData : IStringsProvider
{
    private readonly TypingExercise _exercice;
    private readonly KeyBoardLayoutDto _selectedKeyboard;

    public TypingExerciceStaticData(
        TypingExercise exercice,
        KeyBoardLayoutDto selectedKeyboard)
    {
        _exercice = exercice;
        _selectedKeyboard = selectedKeyboard;
    }

    public async Task<Result<IEnumerable<string>>> GetStringsAsync()
    {
        StaticExerciseVariant? staticExerciceVariant =
            _exercice.Static!.Variants.FirstOrDefault(x => x.Configuration.KeyboardLayout.KeyBoardCode == _selectedKeyboard.KeyBoardCode);

        if (staticExerciceVariant == null)
        {
            return Result<IEnumerable<string>>
                .Fail($"No variant for {_selectedKeyboard.KeyBoardCode}");
        }

        return Result<IEnumerable<string>>
            .Ok(staticExerciceVariant.GeneratedText.Split('↵'));
    }
}

public class TypingExerciceDynamicPseudoWords : IStringsProvider
{
    private readonly TypingExercise _exercice;
    private readonly KeyBoardLayoutDto _selectedKeyboard;
    private readonly IPseudoWordBatchGenerator _pseudoWordBatchGenerator;
    public TypingExerciceDynamicPseudoWords(
        TypingExercise exercice,
        KeyBoardLayoutDto selectedKeyboard,
        IPseudoWordBatchGenerator pseudoWordBatchGenerator)
    {
        _exercice = exercice;
        _selectedKeyboard = selectedKeyboard;
        _pseudoWordBatchGenerator = pseudoWordBatchGenerator;
    }

    // generateur pseudo word requis
    public Task<Result<IEnumerable<string>>> GetStringsAsync()
    {
        throw new NotImplementedException();
    }


}


public class TypingExerciceDynamicWords
{
    public Result<string> GetData { get; set; }

    // repository à la bdd requit => request la bdd avec les critères
}