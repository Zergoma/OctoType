using OctoType.Application.Interfaces;
using OctoType.Application.Models;
using OctoType.Utilities;

namespace OctoType.Presenter;

public class ThemeIconeProvider : IThemeIconeProvider
{
    public string GetIconeCode(IconeThemeState state)
    {
        return state switch
        {
            IconeThemeState.Dark => IconesThemesCodes.MoonDark,
            IconeThemeState.Light => IconesThemesCodes.SunLight,
            IconeThemeState.System => IconesThemesCodes.SystemDark,
            _ => IconesThemesCodes.SystemDark,
        };
    }
}