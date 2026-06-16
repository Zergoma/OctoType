using OctoType.Application.Models;

namespace OctoType.Application.Interfaces;

public interface ITypingExercicesManager
{
    public TypingExercices? Exercice { get; set; }
    Result<TypingExercise> GetExercice(int idx);
    Result<bool> InsertExercice(TypingExercise exercice, int idx);
    Result<bool> AddNewExercice(TypingExercise exercice);
    Result<bool> UpdateExercice(TypingExercise exercice, int idx);
    Result<bool> RemoveExercice(int idx);
}