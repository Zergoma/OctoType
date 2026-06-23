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