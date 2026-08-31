using XyloType.Application;
using XyloType.Application.DTOs;
using XyloType.Application.Interfaces;
using XyloType.Application.Interfaces.Typing;
using XyloType.Application.Models.Typing.Exercices;

namespace XyloType.Infrastructure.Stores;

public class TypingExercicesStorage : ITypingExercicesStorage
{
    private readonly IExerciseSettingsStore _exerciceStrore;
    private readonly ITypingExercicesFileNameProvider _exerciceFilenameProvider;
    private readonly string _exerciceFolder;

    public TypingExercicesStorage(
        IExerciseSettingsStore exerciceStrore,
        IExercicesSettingPathProvider exercicePathProvider,
        ITypingExercicesFileNameProvider exerciceFilenameProvider)
    {
        _exerciceStrore = exerciceStrore;
        _exerciceFilenameProvider = exerciceFilenameProvider;
        _exerciceFolder = exercicePathProvider.ExerciceSettingPath();
    }

    public async Task<Result<TypingExercices>> LoadAsync(KeyboardLayoutEnumDto keyboard)
    {
        Result<string> filenameResult = 
            _exerciceFilenameProvider.GetFileName(keyboard);

        if (!filenameResult.Success)
            return Result<TypingExercices>.Fail(filenameResult.Error);

        string _fullPath = Path.Combine(
            _exerciceFolder,
            filenameResult.GetValue);

        return await _exerciceStrore.LoadAsync(_fullPath);
    }
    
    
    public async Task<Result<bool>> SaveAsync(TypingExercices? exercices)
    {
        if (exercices == null)
            return Result<bool>
                .Fail("exercices are empty");

        
        Result<string> filenameResult =
            _exerciceFilenameProvider.GetFileName(exercices.KeyboardLayout.KeyBoardCode);

        string _fullPath = Path.Combine(
            _exerciceFolder,
            filenameResult.GetValue);

        return await _exerciceStrore.SaveAsync(exercices, _fullPath);
    }
}
