using Microsoft.Extensions.DependencyInjection;

using OctoType.ViewModels.Exercices;
using OctoType.ViewModels.Import;
using OctoType.ViewModels.Typing;


namespace OctoType.ViewModels.DI;

public static class ViewModelsModule
{
    public static IServiceCollection AddViewModelsModule(this IServiceCollection services)
    {
        services.AddTransient<ExerciceGeneratorViewModel>();
        services.AddTransient<ImportBookViewModel>();
        services.AddTransient<ImportWordViewModel>();
        services.AddTransient<TypingViewModel>();
        return services;
    }
}
