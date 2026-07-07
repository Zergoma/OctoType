using OctoType.Application.Interfaces;
using OctoType.Services;

namespace OctoType.DI;

public static class UiServicesModule
{
    public static IServiceCollection AddMauiService(this IServiceCollection services)
    {
        // depends on ViewFactoriesModule
        services.AddTransient<INavigationService, MauiNavigationService>();

        return services;
    }
}
