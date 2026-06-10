using OctoType.MVVM.ViewModels;

namespace OctoType.DI;

public static class ViewModelsModule
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<TypingViewModel>();
        services.AddTransient<ImportBookViewModel>();
        services.AddTransient<ImportWordViewModel>();
        services.AddTransient<ExerciceGeneratorViewModel>();
        return services;
    }
}
