using OctoType.Application.DTOs;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Themes;
using OctoType.Domain.Typing;

namespace OctoType.Infrastructure.Theme.Mappers;

public static class TypingThemeMapper
{
    public static ITypingTheme ToTheme(ThemeDto dto)
    {
        return new TypingTheme(dto);
    }
}

public class TypingTheme : ITypingTheme
{
    private readonly ThemeDto _dto;

    public TypingTheme(ThemeDto dto)
    {
        _dto = dto;
    }

    public TypingStyle GetStyle(TypingCharEnumState state)
    {
        return state switch
        {
            TypingCharEnumState.Pending => Map(_dto.Pending),
            TypingCharEnumState.Current => Map(_dto.Current),
            TypingCharEnumState.Correct => Map(_dto.Correct),
            TypingCharEnumState.CorrectWithError => Map(_dto.CorrectWithError),
            TypingCharEnumState.CurrentWrong => Map(_dto.CurrentWrong),
            _ => Map(_dto.Pending)
        };
    }

    private TypingStyle Map(ThemeStateDto dto)
    {
        return new TypingStyle
        {
            TextColor = dto.TextColor,
            BackgroundColor = dto.BackgroundColor,
            BorderColor = dto.BorderColor,
            BorderThickness = dto.BorderThickness
        };
    }
}