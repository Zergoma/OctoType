using OctoType.Application.Interfaces;
using OctoType.Infrastructure;

namespace OctoType.DI;

public static class InfrastructureMauiModule
{
    public static IServiceCollection AddMauiInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IAssetReader, MauiAssetReader>();
        return services;
    }
}
