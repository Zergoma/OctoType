using OctoType.Application.Models;

namespace OctoType.Application.Interfaces;

public interface IExerciseSettingsStore
{
    Task SaveAsync(TypingExerciceSetting settings, string path);

    Task<TypingExerciceSetting?> LoadAsync(string name, string path);
}