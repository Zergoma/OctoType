using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;

namespace OctoType.Application.Factories;

public class KeyBoardLayoutDtoFactory : IKeyBoardLayoutDtoFactory
{
    public Result<KeyBoardLayoutDto> Create(KeyboardLayoutEnumDto keyboardLayout)
    {
        return keyboardLayout switch
        {
            KeyboardLayoutEnumDto.AzertyFr => Result<KeyBoardLayoutDto>.Ok(new KeyBoardLayoutDto(keyboardLayout, "AzertyFr")),
            KeyboardLayoutEnumDto.QwertyUs => Result<KeyBoardLayoutDto>.Ok(new KeyBoardLayoutDto(keyboardLayout, "QwertyUs")),
            KeyboardLayoutEnumDto.QwertzDe => Result<KeyBoardLayoutDto>.Ok(new KeyBoardLayoutDto(keyboardLayout, "QwertzDe")),
            KeyboardLayoutEnumDto.Bepo => Result<KeyBoardLayoutDto>.Ok(new KeyBoardLayoutDto(keyboardLayout, "Bepo")),
            _ => Result<KeyBoardLayoutDto>.Fail($"No factory found for {keyboardLayout}")
        };
    }
}


