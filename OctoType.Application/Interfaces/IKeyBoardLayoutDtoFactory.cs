using OctoType.Application.DTOs;

namespace OctoType.Application.Interfaces
{
    public interface IKeyBoardLayoutDtoFactory
    {
        Result<KeyBoardLayoutDto> Create(KeyboardLayoutEnumDto keyboardLayout);
    }
}