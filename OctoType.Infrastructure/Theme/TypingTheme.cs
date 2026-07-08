using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Themes;
using OctoType.Application.Models.Typing.Themes;
using OctoType.Domain.Typing;
using OctoType.Infrastructure.Theme.Models;

namespace OctoType.Infrastructure.Theme;

public class TypingTheme : ITypingTheme
{
    private readonly ThemeFileModel _dto;
    private readonly ThemeState _themeState;

    public TypingTheme(
        ThemeFileModel dto,
        ThemeState themeState)
    {
        _dto = dto;
        _themeState = themeState;
    }

    public TypingStyle GetStyle(TypingCharState charState)
    {
        return charState switch
        {
            TypingCharState.Pending => CreateTypingStyle(_dto.Pending),
            TypingCharState.Current => CreateTypingStyle(_dto.Current),
            TypingCharState.Correct => CreateTypingStyle(_dto.Correct),
            TypingCharState.CorrectWithError => CreateTypingStyle(_dto.CorrectWithError),
            TypingCharState.CurrentWrong => CreateTypingStyle(_dto.CurrentWrong),
            _ => CreateTypingStyle(_dto.Pending)
        };
    }

    private TypingStyle CreateTypingStyle(ThemeStateFileModel dto)
    {
        return _themeState switch
        {
            ThemeState.Light => new()
            {
                TextColor = dto.TextColorLight,
                BackgroundColor = dto.BackgroundColorLight,
                BorderColor = dto.BorderColorLight,
                BorderThickness = dto.BorderThicknessLight
            },
            _ => new()
            {
                TextColor = dto.TextColorDark,
                BackgroundColor = dto.BackgroundColorDark,
                BorderColor = dto.BorderColorDark,
                BorderThickness = dto.BorderThicknessDark
            },
        };
    }
}