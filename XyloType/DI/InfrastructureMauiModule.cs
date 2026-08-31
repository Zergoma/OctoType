using XyloType.Application.Interfaces;
using XyloType.Infrastructure;

namespace XyloType.DI;

public static class InfrastructureMauiModule
{
    public static IServiceCollection AddMauiInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IAssetReader, MauiAssetReader>();
        return services;
    }
}
