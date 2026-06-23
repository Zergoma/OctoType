using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;

namespace OctoType.Application.Services;

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