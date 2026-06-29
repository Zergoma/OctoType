using Microsoft.Extensions.DependencyInjection;

using OctoType.ViewModels.ExercicesGenerator;
using OctoType.ViewModels.Import;
using OctoType.ViewModels.Typing;
using OctoType.ViewModels.TypingLauncher;


namespace OctoType.ViewModels.DI;

public static class ViewModelsModule
{
    public static IServiceCollection AddViewModelsModule(this IServiceCollection services)
    {
        services.AddTransient<ExerciceGeneratorViewModel>();
        services.AddTransient<ImportBookViewModel>();
        services.AddTransient<ImportWordViewModel>();
        services.AddTransient<TypingViewModel>();
        services.AddTransient<TypingLauncherViewModel>();
        return services;
    }
}
