using Microsoft.Extensions.DependencyInjection;

using OctoType.ViewModels.Exercices;
using OctoType.ViewModels.Import;


namespace OctoType.ViewModels.DI;

public static class ViewModelsModule
{
    public static IServiceCollection AddViewModelsModule(this IServiceCollection services)
    {
        services.AddTransient<ExerciceGeneratorViewModel>();
        services.AddTransient<ImportBookViewModel>();
        services.AddTransient<ImportWordViewModel>();
        return services;
    }
}
