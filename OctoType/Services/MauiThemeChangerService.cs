using OctoType.Application.Interfaces;
using OctoType.Application.Models;

namespace OctoType.Services;

public class MauiThemeChangerService : IThemeChangerService
{
    public void SetDark()
    {
        App.Current?.UserAppTheme = AppTheme.Dark;
        Preferences.Set("Theme", "Dark");
    }

    public void SetLight()
    {
        App.Current?.UserAppTheme = AppTheme.Light;
        Preferences.Set("Theme", "Light");
    }

    public void SetToSystem()
    {
        App.Current?.UserAppTheme = AppTheme.Unspecified;
        Preferences.Set("Theme", "System");
    }

    public IconeThemeState ApplyUserSelectedTheme()
    {
        string? theme = Preferences.Get("Theme", "System");

        App.Current?.UserAppTheme = theme switch
        {
            "Light" => AppTheme.Light,
            "Dark" => AppTheme.Dark,
            _ => AppTheme.Unspecified
        };

        return theme switch
        {
            "Light" => IconeThemeState.Light,
            "Dark" => IconeThemeState.Dark,
            _ => IconeThemeState.System
        };
    }
}
