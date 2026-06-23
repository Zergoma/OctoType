using OctoType.Application.DTOs;

namespace OctoType.Application.Interfaces
{
    public interface IGenerationTypeSourceAvailableService
    {
        List<GeneratedTypeSourceDto> GetGenerationTypeSourceAvailable();
    }
}