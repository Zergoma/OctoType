using Microsoft.Extensions.DependencyInjection;

using OctoType.Infrastructure.Theme.Availables;

namespace OctoType.Infrastructure.DI;

internal static class InfrastructureThemeModule
{
    public static IServiceCollection AddTheme(this IServiceCollection services)
    {
        services.AddSingleton<AssetThemeAvailable>();
        services.AddSingleton<UserThemeAvailable>();

        return services;
    }
}