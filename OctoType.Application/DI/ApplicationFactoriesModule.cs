using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Factories;
using OctoType.Application.Interfaces;

namespace OctoType.Application.DI;

internal static class ApplicationFactoriesModule
{
    public static IServiceCollection AddOctoTypeApplicationFactories(this IServiceCollection services)
    {
        services.AddTransient<IKeyBoardLayoutDtoFactory, KeyBoardLayoutDtoFactory>();
        services.AddTransient<ITypingExerciceSettingFactory, TypingExerciceSettingFactory>();
        return services;
    }
}