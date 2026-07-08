using OctoType.Application.Models.Themes;

namespace OctoType.Application.Interfaces
{
    public interface IThemeIconeCodeProvider
    {
        string GetIconeCode(ThemeStateConfiguration state);
    }
}