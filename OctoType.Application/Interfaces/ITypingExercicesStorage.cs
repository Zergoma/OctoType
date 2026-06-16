using OctoType.Application.Models;

namespace OctoType.Application.Interfaces;

public interface ITypingExercicesStorage
{
    Task<TypingExercices> LoadAsync();
    Task SaveAsync(TypingExercices? exercices);
}