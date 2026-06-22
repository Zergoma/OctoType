using OctoType.Application.Interfaces;
using OctoType.Application.Models.Typing.Exercices;

using OctoType.Infrastructure.Protos;
using OctoType.Infrastructure.Mappers;
using OctoType.Application;
using OctoType.Application.DTOs;

using Google.Protobuf;

namespace OctoType.Infrastructure.Stores;

public class ProtobufTypingExercisesStore : IExerciseSettingsStore
{
    public async Task<Result<TypingExercices>> LoadAsync(string path)
    {
        if (!File.Exists(path))
            return Result<TypingExercices>
                .Fail($"File doesn't exist: {path}");

        ProtoTypingExerciceList exercicesList = new();
        using (var input = File.OpenRead(path))
        {
            exercicesList = ProtoTypingExerciceList.Parser.ParseFrom(input);
        }

        TypingExercices toReturnList = new();
        foreach (ProtoTypingExercice exerciceItem in exercicesList.Exercices)
        {
            TypingExercise exercice = new()
            {
                Name = exerciceItem.Name,
                Description = exerciceItem.Description,
                Language =
                    string.IsNullOrWhiteSpace(exerciceItem.Language)
                    ? null
                    : exerciceItem.Language
            };

            // static exercice
            if (exerciceItem.ExerciceTypeCase is ProtoTypingExercice.ExerciceTypeOneofCase.StaticExercice)
            {
                TypingExerciseStatic typingExerciceStatic = new();

                // variants
                foreach (ProtoStaticExerciceVariant variantsItem in exerciceItem.StaticExercice.Variants)
                {
                    StaticExerciseVariant staticExerciceVariant = new()
                    {
                        GeneratedText = variantsItem.GeneratedText
                    };

                    ProtoTypingExerciceConfiguration variantConfig = variantsItem.Configuration;
                    ProtoKeyboardLayout variantConfigBeyboard = variantsItem.Configuration.KeyboardLayout;

                    Result<KeyboardLayoutEnumDto> keyboradLayoutMapResult
                        = variantConfigBeyboard.Layout.MapToDtoEnum();

                    if (!keyboradLayoutMapResult.Success)
                    {
                        return Result<TypingExercices>
                            .Fail(keyboradLayoutMapResult.Error);
                    }

                    TypingExerciseConfiguration config = new()
                    {
                        AllowedLetters = variantConfig.AllowedCharacters,
                        KeyboardLayout = new KeyBoardLayoutDto(keyboradLayoutMapResult.GetValue, variantConfigBeyboard.Name),
                    };

                    staticExerciceVariant.Configuration = config;


                    typingExerciceStatic.Variants.Add(staticExerciceVariant);
                }

                exercice.Static = typingExerciceStatic;
                toReturnList.Exercices.Add(exercice);
            }

            if (exerciceItem.ExerciceTypeCase is ProtoTypingExercice.ExerciceTypeOneofCase.DynamicExercice)
            {
                TypingExerciseDynamic typingExerciceDynamic = new();

                foreach (ProtoTypingExerciceConfiguration exerciceConfigurationItem in exerciceItem.DynamicExercice.Configuration)
                {
                    ProtoTypingExerciceConfiguration variantConfig = exerciceConfigurationItem;
                    ProtoKeyboardLayout variantConfigBeyboard = exerciceConfigurationItem.KeyboardLayout;

                    Result<KeyboardLayoutEnumDto> keyboradLayoutMapResult
                        = variantConfigBeyboard.Layout.MapToDtoEnum();

                    if (!keyboradLayoutMapResult.Success)
                    {
                        return Result<TypingExercices>
                            .Fail(keyboradLayoutMapResult.Error);
                    }

                    TypingExerciseConfiguration config = new()
                    {
                        AllowedLetters = variantConfig.AllowedCharacters,
                        KeyboardLayout = new KeyBoardLayoutDto(keyboradLayoutMapResult.GetValue, variantConfigBeyboard.Name),
                    };

                    typingExerciceDynamic.Configurations.Add(config);
                }

                exercice.Dynamic = typingExerciceDynamic;
                toReturnList.Exercices.Add(exercice);
            }

            if(exercice.Dynamic == null && 
                exercice.Static == null)
            {
                return Result<TypingExercices>
                    .Fail("Static and Dynamic fields: both are empty");
            }
        }
        return Result<TypingExercices>.Ok(toReturnList);
    }

    public async Task<Result<bool>> SaveAsync(TypingExercices settings, string path)
    {
        ProtoTypingExerciceList exercicesList = new();

        foreach (TypingExercise item in settings.Exercices)
        {
            ProtoTypingExercice typingExo = new()
            {
                Name = item.Name,
                Description = item.Description
            };

            if (item.Language is not null)
            {
                typingExo.Language = item.Language;
            }

            if (item.Static == null && item.Dynamic == null)
            {
                return Result<bool>
                    .Fail("Exercice must have a dynamic OR a static part");
            }

            if (item.Static != null && item.Dynamic != null)
            {
                return Result<bool>
                    .Fail("Exercice have a dynamic AND a static part: only one of them is allowed");
            }

            if (item.Static is TypingExerciseStatic exoStatic)
            {
                ProtoExerciceStatic staticExercicesList = new();

                foreach (StaticExerciseVariant itemVariant in exoStatic.Variants)
                {
                    KeyBoardLayoutDto itemKeyboardConfig = itemVariant.Configuration.KeyboardLayout;
                    string allowedLettersConfig = itemVariant.Configuration.AllowedLetters;
                    string generatedText = itemVariant.GeneratedText;

                    // keyboard type mapping (azerty, qwertry, etc)
                    var MappedKeyboardTypeResult = itemKeyboardConfig.KeyBoardCode.MapToPbEnum();
                    if (!MappedKeyboardTypeResult.Success)
                    {
                        return Result<bool>
                            .Fail(MappedKeyboardTypeResult.Error);
                    }

                    // Exercice configuration
                    // allowed letters + keyboard type
                    ProtoTypingExerciceConfiguration typingConf = new()
                    {
                        AllowedCharacters = allowedLettersConfig,
                        KeyboardLayout = new ProtoKeyboardLayout()
                        {
                            Name = itemKeyboardConfig.KeyBoardHumanFriendly,
                            Layout = MappedKeyboardTypeResult.GetValue
                        },
                    };

                    ProtoStaticExerciceVariant exerciceStatic = new()
                    {
                        GeneratedText = generatedText,
                        Configuration = typingConf
                    };

                    staticExercicesList.Variants.Add(exerciceStatic);
                }
                
                // add static exercice to current exercice
                typingExo.StaticExercice = staticExercicesList;
            }

            if (item.Dynamic is TypingExerciseDynamic exoDynamic)
            {
                ProtoExerciceDynamic dynamicExercicesList = new();

                foreach (TypingExerciseConfiguration itemConfiguration in exoDynamic.Configurations)
                {
                    KeyBoardLayoutDto itemKeyboardConfig = itemConfiguration.KeyboardLayout;
                    string allowedLettersConfig = itemConfiguration.AllowedLetters;

                    // keyboard type mapping (azerty, qwertry, etc)
                    var MappedKeyboardTypeResult = itemKeyboardConfig.KeyBoardCode.MapToPbEnum();
                    if (!MappedKeyboardTypeResult.Success)
                    {
                        return Result<bool>
                            .Fail(MappedKeyboardTypeResult.Error);
                    }

                    // Exercice configuration
                    // allowed letters + keyboard type
                    ProtoTypingExerciceConfiguration typingConf = new()
                    {
                        AllowedCharacters = allowedLettersConfig,
                        KeyboardLayout = new ProtoKeyboardLayout()
                        {
                            Name = itemKeyboardConfig.KeyBoardHumanFriendly,
                            Layout = MappedKeyboardTypeResult.GetValue
                        },
                    };

                    dynamicExercicesList.Configuration.Add(typingConf);
                    
                }
                
                // add dynamic exercice to current exercice
                typingExo.DynamicExercice = dynamicExercicesList;
            }

            exercicesList.Exercices.Add(typingExo);
        }


        using (var output = File.Create(path))
        {
            exercicesList.WriteTo(output);
        }

        return Result<bool>.Ok(true);
    }
}
