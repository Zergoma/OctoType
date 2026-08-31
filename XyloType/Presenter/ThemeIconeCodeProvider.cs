using XyloType.Application.Interfaces;
using XyloType.Application.Models.Themes;
using XyloType.Utilities;

namespace XyloType.Presenter;

public class ThemeIconeCodeProvider : IThemeIconeCodeProvider
{
    public string GetIconeCode(ThemeStateConfiguration state)
    {
        return state switch
        {
            ThemeStateConfiguration.Dark => IconesThemesCodes.MoonDark,
            ThemeStateConfiguration.Light => IconesThemesCodes.SunLight,
            ThemeStateConfiguration.System => IconesThemesCodes.SystemDark,
            _ => IconesThemesCodes.SystemDark,
        };
    }
}