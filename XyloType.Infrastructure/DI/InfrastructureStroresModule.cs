using Microsoft.Extensions.DependencyInjection;

using XyloType.Application.Interfaces;
using XyloType.Application.Interfaces.Typing;
using XyloType.Infrastructure.Stores;

namespace XyloType.Infrastructure.DI;

internal static class InfrastructureStroresModule
{
    public static IServiceCollection AddStrores(this IServiceCollection services)
    {
        //services.AddTransient<IExerciseSettingsStore, JsonTypingExercisesStore>();
        services.AddTransient<IExerciseSettingsStore, ProtobufTypingExercisesStore>(); 


        services.AddTransient<ITypingExercicesStorage, TypingExercicesStorage>();
        


        return services;
    }
}
