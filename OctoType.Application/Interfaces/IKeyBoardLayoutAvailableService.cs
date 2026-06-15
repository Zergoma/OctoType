using OctoType.Application.DTOs;

namespace OctoType.Application.Interfaces
{
    public interface IKeyBoardLayoutAvailableService
    {
        List<KeyBoardLayoutDto> GetKeyBoardAvailable();
    }
}