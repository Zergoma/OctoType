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
        services.AddTransient<ImportBookViewModel>();
        services.AddTransient<ImportWordViewModel>();
        services.AddTransient<TypingLauncherViewModel>();
        return services;
    }
}
