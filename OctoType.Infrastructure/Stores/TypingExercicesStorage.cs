using OctoType.Application.Interfaces;
using OctoType.Application.Models;

namespace OctoType.Infrastructure.Stores;

public class TypingExercicesStorage : ITypingExercicesStorage
{
    private readonly IExerciseSettingsStore _exerciceStrore;
    private readonly IExercicesSettingPathProvider _exercicePathProvider;
    
    private readonly string _fullPath;

    public TypingExercicesStorage(
        IExerciseSettingsStore exerciceStrore,
        IExercicesSettingPathProvider exercicePathProvider)
    {
        _exerciceStrore = exerciceStrore;
        _exercicePathProvider = exercicePathProvider;
        string exerciceFolder = _exercicePathProvider.ExerciceSettingPath();
        _fullPath = Path.Combine(
            exerciceFolder,
            "Exercices.json");
    }
    
    public async Task<TypingExercices> LoadAsync()
    {
        TypingExercices? loadResu =
            await _exerciceStrore.LoadAsync(_fullPath) ?? new TypingExercices();

        return loadResu;
    }
    
    public async Task SaveAsync(TypingExercices? exercices)
    {
        if (exercices == null)
            return;
        
        await _exerciceStrore.SaveAsync(exercices, _fullPath);
    }
}