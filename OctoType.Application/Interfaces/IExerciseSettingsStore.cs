using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.Interfaces;

public interface IExerciseSettingsStore
{
    Task SaveAsync(TypingExercices settings, string path);

    Task<TypingExercices?> LoadAsync(string path);
}