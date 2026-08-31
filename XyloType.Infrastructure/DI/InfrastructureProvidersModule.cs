using Microsoft.Extensions.DependencyInjection;

using XyloType.Application.Interfaces;
using XyloType.Infrastructure.Providers;
using XyloType.Infrastructure.Providers.Windows;

namespace XyloType.Infrastructure.DI;

internal static class InfrastructureProvidersModule
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddTransient<IThemePathProvider, ThemePathProvider>();
        services.AddTransient<IImportFilePathProvider, ImportFilePathProvider>();
        services.AddTransient<IFileCopyProvider, FileCopyProvider>();
        services.AddTransient<IExercicesSettingPathProvider, ExercicesSettingPathProvider>();
        services.AddTransient<IGetNextInRange, GetNextInRangeProvider>();


        //services.AddTransient<ITypingExercicesFileNameProvider, JsonTypingExercicesFileNameProvider>();
        services.AddTransient<ITypingExercicesFileNameProvider, PbTypingExercicesFileNameProvider>();
        

        // Specific Windows
        services.AddTransient<IKeyboardLayoutDetector, WindowsKeyboardLayoutDetector>();


        return services;
    }
}
