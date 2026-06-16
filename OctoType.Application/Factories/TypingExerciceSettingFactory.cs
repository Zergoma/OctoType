using OctoType.Application.Interfaces;
using OctoType.Application.Models;
using OctoType.Application.UseCases;

namespace OctoType.Application.Factories;

public class TypingExerciceSettingFactory : ITypingExerciceSettingFactory
{
    public TypingExercise GenerateStaticTypingExercices(
        TypingExerciseCreateParameters  typingExerciceSetting,
        string generatedText)
    {
        var (baseSettings, baseExerciceConfig) 
            = GenerateCommonBaseConfiguration(typingExerciceSetting);
        
        TypingExerciseStatic staticsetting = new()
        {
            Variants =
            [
                new StaticExerciseVariant()
                {
                    Configuration = baseExerciceConfig,
                    GeneratedText = generatedText,
                }
            ]
        };
        baseSettings.Static = staticsetting;
        return baseSettings;
    }

    public TypingExercise GenerateDynamicTypingExercices(TypingExerciseCreateParameters typingExerciceSetting)
    {
        var (baseSettings, baseExerciceConfig) 
            = GenerateCommonBaseConfiguration(typingExerciceSetting);


        TypingExerciseDynamic dynamicSetting = new()
        {
            Configurations = [baseExerciceConfig]
        };
        baseSettings.Dynamic = dynamicSetting;
        return baseSettings;
    }
    
    private static (TypingExercise, TypingExerciseConfiguration) GenerateCommonBaseConfiguration(
        TypingExerciseCreateParameters typingExerciceSetting)
    {
        TypingExercise baseSettings = new()
        {
            Name = typingExerciceSetting.Name,
            Description = typingExerciceSetting.Description,
        };

        if (typingExerciceSetting.Language is { } language)
        {
            baseSettings.Language = language;
        }

        TypingExerciseConfiguration baseExerciceConfig = new()
        {
            KeyboardLayout = typingExerciceSetting.KeyBoardLayoutDto,
            AllowedLetters = typingExerciceSetting.AllowedLetters
        };
        
        return (baseSettings, baseExerciceConfig);
    }
}