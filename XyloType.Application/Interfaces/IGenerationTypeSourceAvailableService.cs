using XyloType.Application.DTOs;

namespace XyloType.Application.Interfaces
{
    public interface IGenerationTypeSourceAvailableService
    {
        List<GeneratedTypeSourceDto> GetGenerationTypeSourceAvailable();
    }
}