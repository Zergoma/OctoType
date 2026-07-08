using OctoType.Application.Interfaces;
using OctoType.Application.Models.Themes;

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

    public ThemeStateConfiguration ApplyUserSelectedTheme()
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
            "Light" => ThemeStateConfiguration.Light,
            "Dark" => ThemeStateConfiguration.Dark,
            _ => ThemeStateConfiguration.System
        };
    }

    public ThemeState GetTheme()
    {
        return (App.Current?.RequestedTheme ?? AppTheme.Dark) switch
        {
            AppTheme.Light => ThemeState.Light,
            _ => ThemeState.Dark,
        };
    }
}
