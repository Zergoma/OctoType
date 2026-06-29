using OctoType.Application;
using OctoType.Application.DTOs;

using static OctoType.Infrastructure.Protos.ProtoKeyboardLayout.Types;

namespace OctoType.Infrastructure.Mappers;

public static class KeyboardDtoToProtoEnumMapper
{
    public static Result<ProtoKeyboardLayoutType> MapToPbEnum(this KeyboardLayoutEnumDto keyboardDtoTypeEnum)
    {
        return keyboardDtoTypeEnum switch
        {
            KeyboardLayoutEnumDto.AzertyFr => Result<ProtoKeyboardLayoutType>.Ok(ProtoKeyboardLayoutType.Azertyfr),
            KeyboardLayoutEnumDto.QwertyUs => Result<ProtoKeyboardLayoutType>.Ok(ProtoKeyboardLayoutType.Qwertyus),
            KeyboardLayoutEnumDto.QwertzDe => Result<ProtoKeyboardLayoutType>.Ok(ProtoKeyboardLayoutType.Qwertzde),
            KeyboardLayoutEnumDto.Bepo => Result<ProtoKeyboardLayoutType>.Ok(ProtoKeyboardLayoutType.Bepo),
            _ => Result<ProtoKeyboardLayoutType>
                .Fail($"{keyboardDtoTypeEnum} have no mapping to protobuf format yet implemented")
        };
    }

    public static Result<KeyboardLayoutEnumDto> MapToDtoEnum(this ProtoKeyboardLayoutType keyboardPbTypeEnum)
    {
        return keyboardPbTypeEnum switch
        {
            ProtoKeyboardLayoutType.Azertyfr => Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.AzertyFr),
            ProtoKeyboardLayoutType.Qwertyus => Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.QwertyUs),
            ProtoKeyboardLayoutType.Qwertzde => Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.QwertzDe),
            ProtoKeyboardLayoutType.Bepo=> Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.Bepo),
            _ => Result<KeyboardLayoutEnumDto>
                .Fail($"{keyboardPbTypeEnum} have no mapping to dto format yet implemented")
        };
    }
}
