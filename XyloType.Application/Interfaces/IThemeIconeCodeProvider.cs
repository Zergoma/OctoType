using XyloType.Application.Models.Themes;

namespace XyloType.Application.Interfaces
{
    public interface IThemeIconeCodeProvider
    {
        string GetIconeCode(ThemeStateConfiguration state);
    }
}