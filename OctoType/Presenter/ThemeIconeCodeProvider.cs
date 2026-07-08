using OctoType.Application.Interfaces;
using OctoType.Application.Models.Themes;
using OctoType.Utilities;

namespace OctoType.Presenter;

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