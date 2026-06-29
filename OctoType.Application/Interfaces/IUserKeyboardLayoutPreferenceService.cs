using OctoType.Application.DTOs;

namespace OctoType.Application.Interfaces
{
    public interface IUserKeyboardLayoutPreferenceService
    {
        Result<int> GetKeyboardType();
        void SetKeyboardType(int keyBoardLayoutDtoId);
    }
}