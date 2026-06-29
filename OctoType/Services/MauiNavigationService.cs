using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Factories;

namespace OctoType.Services;

public class MauiNavigationService : INavigationService
{
    private readonly ITypingViewFactory _typingViewFactory;
    private readonly IExerciceGeneratorViewFactory _exerciceViewFactory;

    public MauiNavigationService(
        ITypingViewFactory typingViewFactory,
        IExerciceGeneratorViewFactory exerciceViewFactory)
    {
        _typingViewFactory = typingViewFactory;
        _exerciceViewFactory = exerciceViewFactory;
    }

    public async Task<Result<bool>> NavigateToExerciceGeneratorAsync()
    {
        var exerciceGeneratorViewResult = 
            await _exerciceViewFactory.CreateExerciceGeneratorView();

        if (!exerciceGeneratorViewResult.Success)
        {
            return Result<bool>.Fail(exerciceGeneratorViewResult.Error);
        }

        await Shell.Current.Navigation.PushAsync(exerciceGeneratorViewResult.GetValue);

        return Result<bool>.Ok(true);
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
