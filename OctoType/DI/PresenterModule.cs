using OctoType.Application.Interfaces;
using OctoType.Presenter;
using OctoType.Services;

namespace OctoType.DI;

public static class PresenterModule
{
    public static IServiceCollection AddPresenters(this IServiceCollection services)
    {
        services.AddTransient<IChoosePath, MauiChooseFilePresenter>();
        
        // TODO
        // move or delete
        // Preferences are not as flexible as expected
        // need more standar way, completely out of maui
        services.AddTransient<IUserKeyboardLayoutPreferenceService, MauiUserKeyboardLayoutPreferenceService>();
        return services;
    }
}
