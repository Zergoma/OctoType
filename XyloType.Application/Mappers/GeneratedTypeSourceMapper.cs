using XyloType.Application.DTOs;
using XyloType.Application.Models.Typing;

namespace XyloType.Application.Mappers;

public static class GeneratedTypeSourceMapper
{
    public static Result<GeneratedTypeSource> ToModel(this GeneratedTypeSourceDto dto)
    {
        return dto switch
        {
            GeneratedTypeSourceDto.PseudoWords => Result<GeneratedTypeSource>.Ok(GeneratedTypeSource.PseudoWords),
            GeneratedTypeSourceDto.Words => Result<GeneratedTypeSource>.Ok(GeneratedTypeSource.Words),
            _ => Result<GeneratedTypeSource>.Fail($"No mapping for {dto}")
        };
    }

    public static Result<GeneratedTypeSourceDto> ToDto(this GeneratedTypeSource model)
    {
        return model switch
        {
            GeneratedTypeSource.PseudoWords => Result<GeneratedTypeSourceDto>.Ok(GeneratedTypeSourceDto.PseudoWords),
            GeneratedTypeSource.Words => Result<GeneratedTypeSourceDto>.Ok(GeneratedTypeSourceDto.Words),
            _ => Result<GeneratedTypeSourceDto>.Fail($"No mapping for {model}")
        };
    }
}
