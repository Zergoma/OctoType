using Microsoft.Extensions.DependencyInjection;

using XyloType.Infrastructure.Theme.Availables;

namespace XyloType.Infrastructure.DI;

internal static class InfrastructureThemeModule
{
    public static IServiceCollection AddTheme(this IServiceCollection services)
    {
        services.AddSingleton<AssetThemeAvailable>();
        services.AddSingleton<UserThemeAvailable>();

        return services;
    }
}