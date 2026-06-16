using OctoType.Application.Models;

namespace OctoType.Application.Interfaces;

public interface IExerciseSettingsStore
{
    Task SaveAsync(TypingExercices settings, string path);

    Task<TypingExercices?> LoadAsync(string path);
}