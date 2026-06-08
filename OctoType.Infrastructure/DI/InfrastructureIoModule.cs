using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Infrastructure.IO;

namespace OctoType.Infrastructure.DI;

internal static class InfrastructureIoModule
{
    public static IServiceCollection AddIo(this IServiceCollection services)
    {
        services.AddTransient<IWordStreamReader, TextFileWordReader>();
        return services;
    }
}