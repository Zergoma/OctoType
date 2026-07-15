using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.Interfaces.Typing;

public interface ITypingExercicesManager
{
    public TypingExercices? Exercices { get; set; }
    Result<TypingExercise> GetExercice(int idx);
    Result<bool> InsertExercice(TypingExercise exercice, int idx);
    Result<bool> AddNewExercice(TypingExercise exercice);

    Result<bool> UpdateExercice(TypingExercise exercice, int idx);
    Result<bool> UpdateExercice(TypingExercise exercice);
    
    Result<bool> RemoveExercice(int idx);
    Result<bool> RemoveExercice(Guid id);
}