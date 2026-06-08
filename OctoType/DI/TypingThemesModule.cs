using OctoType.Infrastructure.Themes;
using OctoType.Interfaces;

namespace OctoType.DI;

public static class TypingThemesModule
{
    public static IServiceCollection AddTypingThemes(this IServiceCollection services)
    {
        services.AddSingleton<AssetThemesLoader>();
        services.AddSingleton<UserThemesLoader>();

        services.AddSingleton<AssetThemeAvailable>();
        services.AddSingleton<UserThemeAvailable>();
        

        services.AddSingleton<ITypingThemeRepository, TypingThemeRepository>();

        return services;
    }
}