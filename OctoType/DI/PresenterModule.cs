using OctoType.Application.Interfaces;
using OctoType.Presenter;

namespace OctoType.DI;

public static class PresenterModule
{
    public static IServiceCollection AddPresenters(this IServiceCollection services)
    {
        services.AddTransient<IChoosePath, MauiChooseFilePresenter>();
        return services;
    }
}
