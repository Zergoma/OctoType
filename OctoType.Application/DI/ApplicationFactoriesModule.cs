using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Factories;
using OctoType.Application.Interfaces;

namespace OctoType.Application.DI;

static internal class ApplicationFactoriesModule
{
    public static IServiceCollection AddOctoTypeApplicationFactories(this IServiceCollection services)
    {
        services.AddTransient<IKeyBoardLayoutDtoFactory, KeyBoardLayoutDtoFactory>(); 
        return services;
    }
}