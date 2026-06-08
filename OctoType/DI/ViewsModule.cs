using OctoType.Application.Interfaces;
using OctoType.MVVM.Views;
using OctoType.Presenter;

namespace OctoType.DI;

public static class ViewsModule
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddTransient<TypingView>();        
        services.AddTransient<ImportWordView>();        
        services.AddTransient<ImportBookView>();        
        return services;
    }
}
