using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Themes;
using OctoType.Domain.Typing;
using OctoType.Infrastructure.Theme.Models;

namespace OctoType.Infrastructure.Theme;

public class TypingTheme : ITypingTheme
{
    private readonly ThemeFileModel _dto;

    public TypingTheme(ThemeFileModel dto)
    {
        _dto = dto;
    }

    public TypingStyle GetStyle(TypingCharState state)
    {
        return state switch
        {
            TypingCharState.Pending => Map(_dto.Pending),
            TypingCharState.Current => Map(_dto.Current),
            TypingCharState.Correct => Map(_dto.Correct),
            TypingCharState.CorrectWithError => Map(_dto.CorrectWithError),
            TypingCharState.CurrentWrong => Map(_dto.CurrentWrong),
            _ => Map(_dto.Pending)
        };
    }

    private TypingStyle Map(ThemeStateFileModel dto)
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