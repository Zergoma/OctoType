using OctoType.Application;
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

    public async Task<Result<bool>> NavigateToTypingExerciseAsync(IStringsProvider stringProvider)
    {
        Result<ContentPage> typingviewResult =
            await _typingViewFactory.CreateTypingViewAsync(stringProvider, this);
        if(!typingviewResult.Success)
        {
            return Result<bool>.Fail(typingviewResult.Error);
        }

        await Shell.Current.Navigation.PushAsync(typingviewResult.GetValue);

        return Result<bool>.Ok(true);
    }

    public async Task PopBackAsync()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
