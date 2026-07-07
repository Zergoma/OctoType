using OctoType.Factories;

namespace OctoType.DI;

public static class ViewFactoriesModule
{
    public static IServiceCollection AddMauiViewFactories(this IServiceCollection services)
    {
        services.AddTransient<IExerciceGeneratorViewFactory, ExerciceGeneratorViewFactory>();
        services.AddTransient<ITypingViewFactory, TypingViewFactory>(); 
        services.AddTransient<IStatisticViewFactory, StatisticViewFactory>();

        return services;
    }
}
