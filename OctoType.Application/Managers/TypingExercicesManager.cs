using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.Managers;

public class TypingExercicesManager : ITypingExercicesManager
{
    public TypingExercices? Exercice { get; set; }
    public int ExercicesCount => Exercice?.Exercices.Count ?? 0;

    public Result<TypingExercise> GetExercice(int idx)
    {
        var checkResu = CheckCoherence(idx);
        return checkResu.Success
            ? Result<TypingExercise>.Ok(Exercice!.Exercices[idx])
            : Result<TypingExercise>.Fail(checkResu.Error);
    }

    public Result<bool> InsertExercice(TypingExercise exercice, int idx)
    {
        var checkResu = CheckCoherence(idx);
        if (!checkResu.Success)
            return Result<bool>.Fail(checkResu.Error);

        Exercice!.Exercices.Insert(idx, exercice);
        return Result<bool>.Ok(true);
    }

    public Result<bool> AddNewExercice(TypingExercise exercice)
    {
        if (Exercice == null)
            return Result<bool>.Fail("No exercice found");

        Exercice!.Exercices.Add(exercice);
        return Result<bool>.Ok(true);
    }

    public Result<bool> UpdateExercice(TypingExercise exercice, int idx)
    {
        var checkResu = CheckCoherence(idx);
        if (!checkResu.Success)
            return Result<bool>.Fail(checkResu.Error);

        Exercice!.Exercices[idx] = exercice;

        return Result<bool>.Ok(true);
    }

    public Result<bool> RemoveExercice(int idx)
    {
        var checkResu = CheckCoherence(idx);
        if (!checkResu.Success)
            return Result<bool>.Fail(checkResu.Error);

        Exercice!.Exercices.RemoveAt(idx);
        return Result<bool>.Ok(true);
    }

    private Result<bool> CheckCoherence(int idx)
    {
        if (Exercice == null)
            return Result<bool>.Fail("No exercice found");

        if (idx < 0 || idx >= Exercice!.Exercices.Count)
            return Result<bool>.Fail("Idx out of range");

        return Result<bool>.Ok(true);
    }
}