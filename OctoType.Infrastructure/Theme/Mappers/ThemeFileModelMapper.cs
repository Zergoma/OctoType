using OctoType.Application.Interfaces.Typing;
using OctoType.Infrastructure.Theme.Models;

namespace OctoType.Infrastructure.Theme.Mappers;

public static class ThemeFileModelMapper
{
    public static ITypingTheme ToTheme(this ThemeFileModel themeFileModel)
    {
        return new TypingTheme(themeFileModel);
    }   
}
