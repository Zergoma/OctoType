using XyloType.Application.DTOs;

namespace XyloType.Application.Interfaces
{
    public interface IKeyBoardLayoutAvailableService
    {
        List<KeyBoardLayoutDto> GetKeyBoardAvailable();
    }
}