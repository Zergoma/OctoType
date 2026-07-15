using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.Managers;

public class TypingExercicesManager : ITypingExercicesManager
{
    public TypingExercices? Exercices { get; set; }
    public int ExercicesCount => Exercices?.Exercices.Count ?? 0;

    public Result<TypingExercise> GetExercice(int idx)
    {
        var checkResu = CheckCoherence(idx);
        return checkResu.Success
            ? Result<TypingExercise>.Ok(Exercices!.Exercices[idx])
            : Result<TypingExercise>.Fail(checkResu.Error);
    }

    public Result<bool> InsertExercice(TypingExercise exercice, int idx)
    {
        var checkResu = CheckCoherence(idx);
        if (!checkResu.Success)
            return Result<bool>.Fail(checkResu.Error);

        Exercices!.Exercices.Insert(idx, exercice);
        return Result<bool>.Ok(true);
    }

    public Result<bool> AddNewExercice(TypingExercise exercice)
    {
        if (Exercices == null)
            return Result<bool>.Fail("No exercice found");

        TypingExercise? exerciceExistsResult = Exercices!.Exercices.FirstOrDefault(x => x.Id == exercice.Id);
        
        if(exerciceExistsResult!=null)
            return Result<bool>
                .Fail("Exercice id already exist");


        Exercices!.Exercices.Add(exercice);
        return Result<bool>.Ok(true);
    }

    public Result<bool> UpdateExercice(TypingExercise exercice, int idx)
    {
        var checkResu = CheckCoherence(idx);
        if (!checkResu.Success)
            return Result<bool>.Fail(checkResu.Error);

        Exercices!.Exercices[idx] = exercice;

        return Result<bool>.Ok(true);
    }

    public Result<bool> UpdateExercice(TypingExercise exercice)
    {
        TypingExercise? exerciceExistsResult = Exercices!.Exercices.FirstOrDefault(x => x.Id == exercice.Id);

        if(exerciceExistsResult == null)
            return Result<bool>
                .Fail("Exercice doesn't exist");

        exerciceExistsResult.Name = exercice.Name;
        exerciceExistsResult.Description = exercice.Description;
        exerciceExistsResult.AllowedCharacters = exercice.AllowedCharacters;
        exerciceExistsResult.TextDataType = exercice.TextDataType;
        
        return Result<bool>
            .Ok(true);
    }

    public Result<bool> RemoveExercice(int idx)
    {
        var checkResu = CheckCoherence(idx);
        if (!checkResu.Success)
            return Result<bool>.Fail(checkResu.Error);

        Exercices!.Exercices.RemoveAt(idx);
        return Result<bool>.Ok(true);
    }

    public Result<bool> RemoveExercice(Guid id)
    {
        TypingExercise? exerciceExists = Exercices!.Exercices.FirstOrDefault(x => x.Id == id);

        if (exerciceExists == null)
            return Result<bool>
                .Fail("Exercice doesn't exist");

        Exercices!.Exercices.Remove(exerciceExists);
        return Result<bool>.Ok(true);
    }

    private Result<bool> CheckCoherence(int idx)
    {
        if (Exercices == null)
            return Result<bool>.Fail("No exercice found");

        if (idx < 0 || idx >= Exercices!.Exercices.Count)
            return Result<bool>.Fail("Idx out of range");

        return Result<bool>.Ok(true);
    }
}