using XyloType.Application;
using XyloType.Application.DTOs;

namespace XyloType.Application.Interfaces
{
    public interface IKeyBoardLayoutDtoFactory
    {
        Result<KeyBoardLayoutDto> Create(KeyboardLayoutEnumDto keyboardLayout);
    }
}