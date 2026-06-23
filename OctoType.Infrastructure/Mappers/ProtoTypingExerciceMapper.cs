using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Models.Typing;
using OctoType.Application.Models.Typing.Exercices;
using OctoType.Infrastructure.Protos;

using static OctoType.Infrastructure.Protos.ProtoKeyboardLayout.Types;
using static OctoType.Infrastructure.Protos.ProtoTypingTextDataDynamic.Types;

namespace OctoType.Infrastructure.Mappers;

public static class ProtoTypingExerciceMapper
{
    public static Result<TypingExercise> ToModel(ProtoTypingExercice proto)
    {
        TypingExercise typingExercice
            = new()
            {
                Name = proto.Name,
                Description = proto.Description,
            };

        foreach (ProtoTypingExerciceConfiguration protoConfig in proto.ExercicesConfigs)
        {
            Result<KeyboardLayoutEnumDto> keyboardResult = protoConfig.KeyboardLayout.Layout.MapToDtoEnum();
            if(!keyboardResult.Success)
            {
                return Result<TypingExercise>
                    .Fail(keyboardResult.Error);
            }

            TypingExerciseConfiguration currentConfig
                = new()
                {
                    KeyboardLayout
                        = new KeyBoardLayoutDto(
                            keyboardResult.GetValue,
                            protoConfig.KeyboardLayout.Name),

                    TextData
                        = new TypingTextData()
                        {
                            AllowedLetters = protoConfig.TextData.AllowedCharacters,
                        }
                };

            #region Helpers
            ProtoTypingTextDataStatic protoStaticZone = protoConfig.TextData.StaticTextData;
            ProtoTypingTextDataDynamic protoDynamicZone = protoConfig.TextData.DynamicTextData;
            TypingTextData currentTextData = currentConfig.TextData;
            #endregion


            switch (protoConfig.TextData.TextDataTypeCase)
            {
                case ProtoTypingTextData.TextDataTypeOneofCase.StaticTextData:
                    {

                        currentTextData.StaticTextData
                                        = new TypingTextDataStatic()
                                        {
                                            GeneratedText = protoStaticZone.GeneratedText,
                                        };
                        break;
                    }

                case ProtoTypingTextData.TextDataTypeOneofCase.DynamicTextData:
                    {
                        Result<GeneratedTypeSource> generationTypeSourceResult = protoDynamicZone.GeneratedTypeSource.MapToDtoEnum();
                        if(!generationTypeSourceResult.Success)
                        {
                            return Result<TypingExercise>
                                .Fail(generationTypeSourceResult.Error);
                        }


                        currentTextData.DynamicTextData
                            = new TypingTextDataDynamic()
                            {
                                LengthMin = (int)protoDynamicZone.LengthMin,
                                LengthMax = (int)protoDynamicZone.LengthMax,
                                GeneratedTypeSource = generationTypeSourceResult.GetValue,
                            };

                        foreach (var lang in protoDynamicZone.LanguagesSelected)
                        {
                            currentTextData.DynamicTextData.LanguagesSelected.Add(lang);
                        }

                        break;
                    }
                default:
                    return Result<TypingExercise>
                        .Fail($"Case {protoConfig.TextData.TextDataTypeCase} not yet implemented");

            }

            typingExercice.ExerciceConfigs.Add(currentConfig);
        }


        return Result<TypingExercise>
            .Ok(typingExercice);
    }

    public static Result<ProtoTypingExerciceList> ToProtobuf(TypingExercices settings)
    {
        ProtoTypingExerciceList exercicesList = new();

        foreach (TypingExercise typingExercice in settings.Exercices)
        {
            ProtoTypingExercice protoTypingExo = new()
            {
                Name = typingExercice.Name,
                Description = typingExercice.Description
            };

            foreach (TypingExerciseConfiguration item in typingExercice.ExerciceConfigs)
            {
                ProtoTypingExerciceConfiguration protoConfi = new();


                Result<ProtoKeyboardLayoutType> MappedKeyboardTypeResult
                    = item.KeyboardLayout.KeyBoardCode.MapToPbEnum();
                if (!MappedKeyboardTypeResult.Success)
                {
                    return Result<ProtoTypingExerciceList>
                        .Fail(MappedKeyboardTypeResult.Error);
                }

                protoConfi.KeyboardLayout = new()
                {
                    Name = item.KeyboardLayout.KeyBoardHumanFriendly,
                    Layout = MappedKeyboardTypeResult.GetValue
                };

                protoConfi.TextData = new()
                {
                    AllowedCharacters = item.TextData.AllowedLetters
                };

                // TODO
                // extract to extrenal validator
                if (item.TextData.StaticTextData == null && item.TextData.DynamicTextData == null)
                {
                    return Result<ProtoTypingExerciceList>
                            .Fail("Static and dynamic fields are null");
                }

                // TODO
                // extract to extrenal validator
                if (item.TextData.StaticTextData != null && item.TextData.DynamicTextData != null)
                {
                    return Result<ProtoTypingExerciceList>
                            .Fail("Static and dynamic fields are set -> only one is authorized");
                }

                if (item.TextData.StaticTextData is TypingTextDataStatic staticTextData)
                {
                    protoConfi.TextData.StaticTextData
                        = new ProtoTypingTextDataStatic()
                        {
                            GeneratedText
                                = staticTextData.GeneratedText
                        };
                }

                if (item.TextData.DynamicTextData is TypingTextDataDynamic dynamicTextData)
                {
                    Result<ProtoGeneratedTypeSource> sourceTypeResult
                        = dynamicTextData.GeneratedTypeSource.MapToPbEnum();
                    if (!sourceTypeResult.Success)
                    {
                        return Result<ProtoTypingExerciceList>
                            .Fail(sourceTypeResult.Error);
                    }


                    protoConfi.TextData.DynamicTextData
                        = new ProtoTypingTextDataDynamic()
                        {
                            LengthMin = (uint)dynamicTextData.LengthMin,
                            LengthMax = (uint)dynamicTextData.LengthMax,
                            GeneratedTypeSource = sourceTypeResult.GetValue
                        };

                    foreach (string lang in dynamicTextData.LanguagesSelected)
                    {
                        protoConfi.TextData.DynamicTextData.LanguagesSelected.Add(lang);
                    }
                }

                protoTypingExo.ExercicesConfigs.Add(protoConfi);
            }

            exercicesList.Exercices.Add(protoTypingExo);
        }

        return Result<ProtoTypingExerciceList>.Ok(exercicesList);
    }
}
