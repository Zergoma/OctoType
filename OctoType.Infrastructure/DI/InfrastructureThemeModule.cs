using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Infrastructure.IO;
using OctoType.Infrastructure.Theme;

namespace OctoType.Infrastructure.DI;

internal static class InfrastructureThemeModule
{
    public static IServiceCollection AddTheme(this IServiceCollection services)
    {
        services.AddTransient<IWordStreamReader, TextFileWordReader>();

        services.AddSingleton<AssetThemesLoader>();
        services.AddSingleton<UserThemesLoader>();

        services.AddSingleton<AssetThemeAvailable>();
        services.AddSingleton<UserThemeAvailable>();

        services.AddSingleton<ITypingThemeRepository, TypingThemeRepository>();


        return services;
    }
}