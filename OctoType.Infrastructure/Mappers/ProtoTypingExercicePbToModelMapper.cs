using OctoType.Application;
using OctoType.Application.Models.Typing;
using OctoType.Application.Models.Typing.Exercices;
using OctoType.Infrastructure.Protos;

using static OctoType.Infrastructure.Protos.ProtoKeyboardLayout.Types;
using static OctoType.Infrastructure.Protos.ProtoTypingTextDataDynamic.Types;

namespace OctoType.Infrastructure.Mappers;

public static class ProtoTypingExercicePbToModelMapper
{
    private static Result<TypingTextData> PbToModelCreateDynamic(ProtoTypingExercice proto)
    {
        if (proto.TextDataTypeCase != ProtoTypingExercice.TextDataTypeOneofCase.DynamicTextData)
        {
            Result<TypingTextData>.Fail("Not a dynamic");
        }

        Result<GeneratedTypeSource> generationTypeSourceResult
            = proto.DynamicTextData.GeneratedTypeSource.MapToDtoEnum();


        if (!generationTypeSourceResult.Success)
        {
            return Result<TypingTextData>
                .Fail(generationTypeSourceResult.Error);
        }


        var curr = new TypingTextDataDynamic()
        {
            LengthMin = (int)proto.DynamicTextData.LengthMin,
            LengthMax = (int)proto.DynamicTextData.LengthMax,
            GeneratedTypeSource = generationTypeSourceResult.GetValue,
        };

        foreach (var lang in proto.DynamicTextData.LanguagesSelected)
        {
            curr.LanguagesSelected.Add(lang);
        }
        return Result<TypingTextData>.Ok(curr);
    }

    public static Result<TypingExercise> ToModel(ProtoTypingExercice proto)
    {
        TypingExercise typingExercice
            = new()
            {
                Name = proto.Name,
                Description = proto.Description,
                AllowedCharacters = proto.AllowedCharacters,
                Id = new (proto.Id.ToByteArray()),
            };

        Result<TypingTextData> staticDynamicResult = proto.TextDataTypeCase switch
        {
            ProtoTypingExercice.TextDataTypeOneofCase.StaticTextData =>
                Result<TypingTextData>.Ok(new TypingTextDataStatic()
                {
                    GeneratedText = proto.StaticTextData.GeneratedText
                }),

            ProtoTypingExercice.TextDataTypeOneofCase.DynamicTextData =>
                PbToModelCreateDynamic(proto),

            _ => Result<TypingTextData>.Fail("The field is empty, it must be static or dynamic type"),
        };

        if (!staticDynamicResult.Success)
            return Result<TypingExercise>
                .Fail(staticDynamicResult.Error);

        typingExercice.TextDataType = staticDynamicResult.GetValue;


        return Result<TypingExercise>
            .Ok(typingExercice);
    }
}

public static class ProtoTypingExerciceModeltoPbMapper
{
    public static Result<ProtoTypingExerciceList> ToProtobuf(TypingExercices settings)
    {
        ProtoTypingExerciceList exercicesList = new();

        Result<ProtoKeyboardLayoutType> MappedKeyboardTypeResult
                    = settings.KeyboardLayout.KeyBoardCode.MapToPbEnum();
        if (!MappedKeyboardTypeResult.Success)
        {
            return Result<ProtoTypingExerciceList>
                .Fail(MappedKeyboardTypeResult.Error);
        }

        exercicesList.KeyboardLayout = new()
        {
            Name = settings.KeyboardLayout.KeyBoardHumanFriendly,
            Layout = MappedKeyboardTypeResult.GetValue
        };



        foreach (TypingExercise typingExercice in settings.Exercices)
        {
            ProtoTypingExercice protoTypingExo = new()
            {
                Name = typingExercice.Name,
                Description = typingExercice.Description,
                AllowedCharacters = typingExercice.AllowedCharacters,
                Id = Google.Protobuf.ByteString.CopyFrom(typingExercice.Id.ToByteArray()),
            };


            Result<ProtoTypingExercice> MappedToDynamicStaticResu
                = typingExercice.TextDataType switch
                {
                    TypingTextDataStatic staticItem => ModelToPb_Static(protoTypingExo, staticItem),
                    TypingTextDataDynamic dynamicItem => ModelToPb_Dynamic(protoTypingExo, dynamicItem)
                };

            if (!MappedToDynamicStaticResu.Success)
                return Result<ProtoTypingExerciceList>.Fail(MappedToDynamicStaticResu.Error);


            protoTypingExo = MappedToDynamicStaticResu.GetValue;
            exercicesList.Exercices.Add(protoTypingExo);
        }

        return Result<ProtoTypingExerciceList>.Ok(exercicesList);
    }

    private static Result<ProtoTypingExercice> ModelToPb_Static(
        ProtoTypingExercice protoTypingExo,
        TypingTextDataStatic staticItem)
    {
        protoTypingExo.StaticTextData
            = new ProtoTypingTextDataStatic()
            {
                GeneratedText = staticItem.GeneratedText
            };

        return Result<ProtoTypingExercice>
            .Ok(protoTypingExo);
    }

    private static Result<ProtoTypingExercice> ModelToPb_Dynamic(
        ProtoTypingExercice protoTypingExo,
        TypingTextDataDynamic dynamicItem)
    {
        Result<ProtoGeneratedTypeSource> sourceTypeResult
            = dynamicItem.GeneratedTypeSource.MapToPbEnum();

        if (!sourceTypeResult.Success)
        {
            return Result<ProtoTypingExercice>
                .Fail(sourceTypeResult.Error);
        }


        protoTypingExo.DynamicTextData
            = new ProtoTypingTextDataDynamic()
            {
                LengthMin = (uint)dynamicItem.LengthMin,
                LengthMax = (uint)dynamicItem.LengthMax,
                GeneratedTypeSource = sourceTypeResult.GetValue
            };

        foreach (string lang in dynamicItem.LanguagesSelected)
        {
            protoTypingExo.DynamicTextData.LanguagesSelected.Add(lang);
        }

        return Result<ProtoTypingExercice>
            .Ok(protoTypingExo);
    }

}
