using OctoType.Application.Interfaces.Typing;
using OctoType.Infrastructure.Themes;

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