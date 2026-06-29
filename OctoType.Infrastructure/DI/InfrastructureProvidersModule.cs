using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Infrastructure.Providers;
using OctoType.Infrastructure.Providers.Windows;

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
        

        // Specific Windows
        services.AddTransient<IKeyboardLayoutDetector, WindowsKeyboardLayoutDetector>();


        return services;
    }
}
