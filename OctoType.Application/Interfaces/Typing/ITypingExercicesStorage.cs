using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.Interfaces.Typing;

public interface ITypingExercicesStorage
{
    Task<Result<TypingExercices>> LoadAsync();
    Task<Result<bool>> SaveAsync(TypingExercices? exercices);
}