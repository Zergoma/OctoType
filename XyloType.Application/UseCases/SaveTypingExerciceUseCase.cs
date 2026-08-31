using FluentValidation;
using FluentValidation.Results;

using XyloType.Application.Interfaces;
using XyloType.Application.Interfaces.Typing;
using XyloType.Application.Models.Typing.Exercices;

namespace XyloType.Application.UseCases;

public class SaveTypingExerciceUseCase : ISaveTypingExerciceUseCase
{
    private readonly ITypingExerciceSettingFactory _factory;
    private readonly ITypingExercicesStorage _storage;
    private readonly IValidator<TypingExerciseCreateParameters> _typingExerciceSettingValidator;
    private readonly IValidator<TypingExercise> _typingExerciceValidator;

    public SaveTypingExerciceUseCase(
        ITypingExerciceSettingFactory factory,

        ITypingExercicesStorage storage,
        IValidator<TypingExerciseCreateParameters> typingExerciceSettingValidator,
        IValidator<TypingExercise> typingExerciceValidator)
    {
        _factory = factory;
        _storage = storage;
        _typingExerciceSettingValidator = typingExerciceSettingValidator;
        _typingExerciceValidator = typingExerciceValidator;
    }

    public async Task<Result<bool>> SaveNewExerciceAsync(
        TypingExerciseCreateParameters parameters,
        bool isStatic,
        string? generatedText,
        ITypingExercicesManager exerciceManager,
        TypingTextDataDynamic? dynamicTypingTextData)
    {
        ValidationResult validationResu =
            _typingExerciceSettingValidator.Validate(parameters);

        if (!validationResu.IsValid)
        {
            return Result<bool>
                .Fail(validationResu.ToString());
        }

        if (!isStatic && dynamicTypingTextData == null)
        {
            return Result<bool>
                .Fail("Data for dynamic are null");
        }

        TypingExercise typingExerciceSettings = isStatic switch
        {
            true => _factory.GenerateStaticTypingExercices(parameters, generatedText!),
            _ => _factory.GenerateDynamicTypingExercices(parameters, dynamicTypingTextData!),
        };

        // Add fresh new exercice
        exerciceManager.AddNewExercice(typingExerciceSettings);

        // Record all exercices
        return await _storage.SaveAsync(exerciceManager.Exercices);
    }

    public async Task<Result<bool>> UpdateExerciceAsync(
       ITypingExercicesManager exerciceManager,
       TypingExercise exercice)
    {
        // validation
        ValidationResult validationResu =
            _typingExerciceValidator.Validate(exercice);

        if (!validationResu.IsValid)
        {
            return Result<bool>
                .Fail(validationResu.ToString());
        }

        // updating
        exerciceManager.UpdateExercice(exercice);

        // Record all exercices
        return await _storage.SaveAsync(exerciceManager.Exercices);
    }
}