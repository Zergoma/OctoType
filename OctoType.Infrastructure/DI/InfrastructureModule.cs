using Microsoft.Extensions.DependencyInjection;

namespace OctoType.Infrastructure.DI;

public static class InfrastructureModule
{
    public static IServiceCollection AddOctoTypeInfrastructure(this IServiceCollection services)
    {
        services.AddProviders();
        services.AddRepositories();
        services.AddIo();
        services.AddStrores();

        return services;
    }
}
