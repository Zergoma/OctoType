using OctoType.MVVM.Views;

namespace OctoType.DI;

public static class ViewsModule
{
    public static IServiceCollection AddMauiViews(this IServiceCollection services)
    {
        services.AddTransient<TypingView>();
        services.AddTransient<ImportWordView>();
        services.AddTransient<ImportBookView>();
        services.AddTransient<ExerciceGeneratorView>();
        services.AddTransient<TypingLauncherView>();

        return services;
    }
}
