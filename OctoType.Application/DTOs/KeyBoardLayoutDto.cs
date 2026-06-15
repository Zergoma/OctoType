
namespace OctoType.Application.DTOs;

public class KeyBoardLayoutDto
{
    public KeyboardLayoutEnumDto KeyBoardCode { get; init; }
    public string KeyBoardHumanFriendly { get; init; } = string.Empty;

    public KeyBoardLayoutDto(
        KeyboardLayoutEnumDto keyBoardCode,
        string keyBoardHumanFriendly)
    {
        KeyBoardCode = keyBoardCode;
        KeyBoardHumanFriendly = keyBoardHumanFriendly;
    }

    public override string? ToString()
    {
        return KeyBoardHumanFriendly;
    }
}