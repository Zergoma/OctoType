using OctoType.Application.Interfaces;
using OctoType.Factories;

namespace OctoType.Services;

public class MauiNavigationService : INavigationService
{
    private readonly ITypingViewFactory _typingViewFactory;

    public MauiNavigationService(ITypingViewFactory typingViewFactory)
    {
        _typingViewFactory = typingViewFactory;
    }

    public async Task NavigateToTypingExerciseAsync()
    {
        ContentPage typingview = _typingViewFactory.CreateTypingView();
        await Shell.Current.Navigation.PushAsync(typingview);
    }
}
