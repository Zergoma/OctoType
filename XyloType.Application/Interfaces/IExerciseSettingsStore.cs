using XyloType.Application;
using XyloType.Application.Models.Typing.Exercices;

namespace XyloType.Application.Interfaces;

public interface IExerciseSettingsStore
{
    Task<Result<bool>> SaveAsync(TypingExercices settings, string path);

    Task<Result<TypingExercices>> LoadAsync(string path);
}