using XyloType.Application;
using XyloType.Application.Models.Typing;

using static XyloType.Infrastructure.Protos.ProtoTypingTextDataDynamic.Types;

namespace XyloType.Infrastructure.Mappers;

public static class GenerationSourceTypeToProtoEnumMapper
{
    public static Result<ProtoGeneratedTypeSource> MapToPbEnum(this GeneratedTypeSource generationSourceType)
    {
        return generationSourceType switch
        {
            GeneratedTypeSource.PseudoWords => Result<ProtoGeneratedTypeSource>.Ok(ProtoGeneratedTypeSource.Pseudowords),
            GeneratedTypeSource.Words => Result<ProtoGeneratedTypeSource>.Ok(ProtoGeneratedTypeSource.Words),
            _ => Result<ProtoGeneratedTypeSource>
                .Fail($"{generationSourceType} have no mapping to protobuf format yet implemented")
        };
    }

    public static Result<GeneratedTypeSource> MapToDtoEnum(this ProtoGeneratedTypeSource keyboardPbTypeEnum)
    {
        return keyboardPbTypeEnum switch
        {
            ProtoGeneratedTypeSource.Pseudowords => Result<GeneratedTypeSource>.Ok(GeneratedTypeSource.PseudoWords),
            ProtoGeneratedTypeSource.Words => Result<GeneratedTypeSource>.Ok(GeneratedTypeSource.Words),
            _ => Result<GeneratedTypeSource>
                .Fail($"{keyboardPbTypeEnum} have no mapping to dto format yet implemented")
        };
    }
}
