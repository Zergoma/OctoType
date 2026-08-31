using XyloType.Application.DTOs;
using XyloType.Application.Interfaces;

namespace XyloType.Application.Services;

public class GenerationTypeSourceAvailableService : IGenerationTypeSourceAvailableService
{
    public List<GeneratedTypeSourceDto> GetGenerationTypeSourceAvailable()
    {
        return
        [
            GeneratedTypeSourceDto.PseudoWords,
            GeneratedTypeSourceDto.Words
        ];
    }
}