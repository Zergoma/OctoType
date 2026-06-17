using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.Interfaces.Typing;

public interface ITypingExercicesStorage
{
    Task<TypingExercices> LoadAsync();
    Task SaveAsync(TypingExercices? exercices);
}