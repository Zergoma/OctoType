using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Infrastructure.Stores;

public class TypingExercicesStorage : ITypingExercicesStorage
{
    private readonly IExerciseSettingsStore _exerciceStrore;
    private readonly IExercicesSettingPathProvider _exercicePathProvider;
    
    private readonly string _fullPath;

    public TypingExercicesStorage(
        IExerciseSettingsStore exerciceStrore,
        IExercicesSettingPathProvider exercicePathProvider,
        ITypingExercicesFileNameProvider exerciceFilenameProvider)
    {
        _exerciceStrore = exerciceStrore;
        _exercicePathProvider = exercicePathProvider;
        string exerciceFolder = _exercicePathProvider.ExerciceSettingPath();
        
        _fullPath = Path.Combine(
            exerciceFolder,
            exerciceFilenameProvider.GetFileName());
    }

    public async Task<Result<TypingExercices>> LoadAsync()
        => await _exerciceStrore.LoadAsync(_fullPath);
    
    
    public async Task<Result<bool>> SaveAsync(TypingExercices? exercices)
    {
        if (exercices == null)
            return Result<bool>
                .Fail("exercices are empty");

        return await _exerciceStrore.SaveAsync(exercices, _fullPath);
    }
}
