using FluentValidation;
using FluentValidation.Results;

using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.UseCases;

public class SaveTypingExerciceUseCase : ISaveTypingExerciceUseCase
{
    private readonly ITypingExerciceSettingFactory _factory;
    private readonly ITypingExercicesStorage _storage;
    private readonly IValidator<TypingExerciseCreateParameters> _typingExerciceSettingValidator;

    public SaveTypingExerciceUseCase(
        ITypingExerciceSettingFactory factory,

        ITypingExercicesStorage storage,
        IValidator<TypingExerciseCreateParameters> typingExerciceSettingValidator)
    {
        _factory = factory;
        _storage = storage;
        _typingExerciceSettingValidator = typingExerciceSettingValidator;
    }

    public async Task<Result<bool>> ExecuteAsync(
        TypingExerciseCreateParameters parameters,
        bool isStatic,
        string? generatedText,
        ITypingExercicesManager exerciceManager)
    {
        ValidationResult validationResu =
            _typingExerciceSettingValidator.Validate(parameters);

        if (!validationResu.IsValid)
        {
            return Result<bool>
                .Fail(validationResu.ToString());
        }

        TypingExercise typingExerciceSettings = isStatic switch
        {
            true => _factory.GenerateStaticTypingExercices(parameters, generatedText!),
            _ => _factory.GenerateDynamicTypingExercices(parameters),
        };

        exerciceManager.AddNewExercice(typingExerciceSettings);

        await _storage.SaveAsync(exerciceManager.Exercice);
        
        return Result<bool>
            .Ok(true);
    }
}