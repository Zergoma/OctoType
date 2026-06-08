using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Infrastructure.Repositories;

namespace OctoType.Infrastructure.DI;

internal static class InfrastructureRepositoriesModule
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IDactyloRepository, DactyloRepository>();
        return services;
    }
}