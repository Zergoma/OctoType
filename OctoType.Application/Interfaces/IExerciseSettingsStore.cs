using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.Interfaces;

public interface IExerciseSettingsStore
{
    Task<Result<bool>> SaveAsync(TypingExercices settings, string path);

    Task<Result<TypingExercices>> LoadAsync(string path);
}