using XyloType.Application.Interfaces.Typing;
using XyloType.Application.Models.Themes;
using XyloType.Infrastructure.Theme.Models;

namespace XyloType.Infrastructure.Theme.Mappers;

public static class ThemeFileModelMapper
{
    public static ITypingTheme ToTheme(this ThemeFileModel themeFileModel, ThemeState themeState)
    {
        return new TypingTheme(themeFileModel, themeState);
    }   
}
