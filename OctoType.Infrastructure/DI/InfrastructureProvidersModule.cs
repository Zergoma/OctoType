using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Infrastructure.Providers;

namespace OctoType.Infrastructure.DI;

internal static class InfrastructureProvidersModule
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddTransient<IThemePathProvider, ThemePathProvider>();
        services.AddTransient<IImportFilePathProvider, ImportFilePathProvider>();
        services.AddTransient<IFileCopyProvider, FileCopyProvider>();
        services.AddTransient<IExercicesSettingPathProvider, ExercicesSettingPathProvider>();
        
        
        //services.AddTransient<ITypingExercicesFileNameProvider, JsonTypingExercicesFileNameProvider>();
        services.AddTransient<ITypingExercicesFileNameProvider, PbTypingExercicesFileNameProvider>();

        return services;
    }
}
