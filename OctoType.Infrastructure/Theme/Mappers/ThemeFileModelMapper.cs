using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Themes;
using OctoType.Infrastructure.Theme.Models;

namespace OctoType.Infrastructure.Theme.Mappers;

public static class ThemeFileModelMapper
{
    public static ITypingTheme ToTheme(this ThemeFileModel themeFileModel, ThemeState themeState)
    {
        return new TypingTheme(themeFileModel, themeState);
    }   
}
