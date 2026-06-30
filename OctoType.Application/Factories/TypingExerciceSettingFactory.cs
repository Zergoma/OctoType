using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Exercices;
using OctoType.Application.UseCases;

namespace OctoType.Application.Factories;

public class TypingExerciceSettingFactory : ITypingExerciceSettingFactory
{
    private readonly IGuidProvider _guidProvider;

    public TypingExerciceSettingFactory(IGuidProvider guidProvider)
    {
        _guidProvider = guidProvider;
    }

    public TypingExercise GenerateStaticTypingExercices(
        TypingExerciseCreateParameters  typingExerciceSetting,
        string generatedText)
    {
        TypingExercise typingExercice 
            = GenerateCommonBaseConfiguration(typingExerciceSetting);

        typingExercice.TextDataType =
            new TypingTextDataStatic()
            {
                GeneratedText = generatedText
            };
        return typingExercice;
    }

    public TypingExercise GenerateDynamicTypingExercices(
        TypingExerciseCreateParameters typingExerciceSetting,
        TypingTextDataDynamic dynamicTypingTextData)
    {
        TypingExercise typingExercice 
            = GenerateCommonBaseConfiguration(typingExerciceSetting);

        typingExercice.TextDataType = dynamicTypingTextData;
        return typingExercice;
    }
    
    private TypingExercise GenerateCommonBaseConfiguration(
        TypingExerciseCreateParameters typingExerciceSetting) =>
        
        new()
        {
            Name = typingExerciceSetting.Name,
            Description = typingExerciceSetting.Description,
            AllowedCharacters = typingExerciceSetting.AllowedLetters,
            Id = _guidProvider.CreateGuid(),
        };
}