using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Infrastructure.IO;
using OctoType.Infrastructure.Theme.Loaders;
using OctoType.Infrastructure.Theme.Providers;

namespace OctoType.Infrastructure.DI;

internal static class InfrastructureIoModule
{
    public static IServiceCollection AddIo(this IServiceCollection services)
    {
        services.AddTransient<IWordStreamReader, TextFileWordReader>();

        services.AddSingleton<AssetThemesLoader>();
        services.AddSingleton<UserThemesLoader>();

        services.AddSingleton<ITypingThemeProvider, TypingThemeProvider>();
        return services;
    }
}