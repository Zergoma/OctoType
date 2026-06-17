using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Infrastructure.Stores;

namespace OctoType.Infrastructure.DI;

internal static class InfrastructureStroresModule
{
    public static IServiceCollection AddStrores(this IServiceCollection services)
    {
        services.AddTransient<IExerciseSettingsStore, JsonTypingExercisesStore>();
        services.AddTransient<ITypingExercicesStorage, TypingExercicesStorage>();
        return services;
    }
}
