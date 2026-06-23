using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Exercices;
using OctoType.Application.UseCases;

namespace OctoType.Application.Factories;

public class TypingExerciceSettingFactory : ITypingExerciceSettingFactory
{
    public TypingExercise GenerateStaticTypingExercices(
        TypingExerciseCreateParameters  typingExerciceSetting,
        string generatedText)
    {
        var (typingExercice, typingExerciceConfiguration) 
            = GenerateCommonBaseConfiguration(typingExerciceSetting);

        typingExerciceConfiguration.TextData.StaticTextData = new()
        {
            GeneratedText = generatedText
        };

        typingExercice.ExerciceConfigs.Add(typingExerciceConfiguration);
        return typingExercice;
    }

    public TypingExercise GenerateDynamicTypingExercices(
        TypingExerciseCreateParameters typingExerciceSetting,
        TypingTextDataDynamic dynamicTypingTextData)
    {
        var (typingExercice, typingExerciceConfiguration) 
            = GenerateCommonBaseConfiguration(typingExerciceSetting);


        typingExerciceConfiguration.TextData.DynamicTextData = dynamicTypingTextData;


        typingExercice.ExerciceConfigs.Add(typingExerciceConfiguration);
        return typingExercice;
    }
    
    private static (TypingExercise, TypingExerciseConfiguration) GenerateCommonBaseConfiguration(
        TypingExerciseCreateParameters typingExerciceSetting)
    {
        TypingExercise baseSettings = new()
        {
            Name = typingExerciceSetting.Name,
            Description = typingExerciceSetting.Description,
        };

        TypingExerciseConfiguration baseExerciceConfig = new()
        {
            KeyboardLayout = typingExerciceSetting.KeyBoardLayoutDto,
            TextData = new TypingTextData()
            {
                AllowedLetters = typingExerciceSetting.AllowedLetters,
            }
        };
        
        return (baseSettings, baseExerciceConfig);
    }
}