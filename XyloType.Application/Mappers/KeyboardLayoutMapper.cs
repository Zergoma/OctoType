using XyloType.Application;
using XyloType.Application.DTOs;
using XyloType.Application.Factories;
using XyloType.Domain.Enums;

namespace XyloType.Application.Mappers;

static public class KeyboardLayoutMapper
{
    static private readonly KeyBoardLayoutDtoFactory s_factoryKeyboardDto = new();
    public static Result<string> ToString(this KeyboardLayout keylayout)
    {
        return keylayout switch
        {
            KeyboardLayout.AzertyFr => Result<string>.Ok("AzertyFr"),
            KeyboardLayout.QwertyUs => Result<string>.Ok("QwertyUs"),
            KeyboardLayout.QwertzDe => Result<string>.Ok("QwertzDe"),
            KeyboardLayout.Bepo => Result<string>.Ok("Bepo"),
            _ => Result<string>.Fail($"Layout {keylayout} is not implemented")
        };
    }

    public static Result<KeyboardLayout> ToDomainEnum(this KeyboardLayoutEnumDto keyboard)
    {
        return keyboard switch
        {
            KeyboardLayoutEnumDto.AzertyFr => Result<KeyboardLayout>.Ok(KeyboardLayout.AzertyFr),
            KeyboardLayoutEnumDto.QwertyUs => Result<KeyboardLayout>.Ok(KeyboardLayout.QwertyUs),
            KeyboardLayoutEnumDto.QwertzDe => Result<KeyboardLayout>.Ok(KeyboardLayout.QwertzDe),
            KeyboardLayoutEnumDto.Bepo => Result<KeyboardLayout>.Ok(KeyboardLayout.Bepo),
            _ => Result<KeyboardLayout>.Fail($"No mapping found for {keyboard}")
        };
    }

    private static Result<KeyboardLayoutEnumDto> ToAppEnum(this KeyboardLayout keyboard)
    {
        return keyboard switch
        {
            KeyboardLayout.AzertyFr => Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.AzertyFr),
            KeyboardLayout.QwertyUs => Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.QwertyUs),
            KeyboardLayout.QwertzDe => Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.QwertzDe),
            KeyboardLayout.Bepo => Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.Bepo),
            _ => Result<KeyboardLayoutEnumDto>.Fail($"No mapping found for {keyboard}"),
        };
    }

    public static Result<KeyBoardLayoutDto> ToDto(this KeyboardLayout keylayout)
    {
        Result<KeyboardLayoutEnumDto> entityToAppEnumResult = ToAppEnum(keylayout);
        if (!entityToAppEnumResult.Success)
        {
            return Result<KeyBoardLayoutDto>.Fail(entityToAppEnumResult.Error);
        }

        return s_factoryKeyboardDto.Create(entityToAppEnumResult.Value!);
    }

    public static Result<string> ToHumanFriendly(this KeyboardLayoutEnumDto keylayout)
    {
        return keylayout switch
        {
            KeyboardLayoutEnumDto.AzertyFr => Result<string>.Ok("AzertyFr"),
            KeyboardLayoutEnumDto.QwertyUs => Result<string>.Ok("QwertyUs"),
            KeyboardLayoutEnumDto.QwertzDe => Result<string>.Ok("QwertzDe"),
            KeyboardLayoutEnumDto.Bepo => Result<string>.Ok("Bepo"),
            _ => Result<string>.Fail($"Layout {keylayout} is not implemented")
        };
    }
}
