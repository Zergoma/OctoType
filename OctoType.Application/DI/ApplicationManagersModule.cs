using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Application.Managers;
using OctoType.Application.Services;

namespace OctoType.Application.DI;

internal static class ApplicationManagersModule
{
    public static IServiceCollection AddOctoTypeApplicationManagers(this IServiceCollection services)
    {
        services.AddTransient<ITypingExercicesManager, TypingExercicesManager>();
        services.AddTransient<IKeyboardKeyLocatorManager, KeyboardKeyLocatorManager>(); //IKeyboardKeysLocator

        return services;
    }
}