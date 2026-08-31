using XyloType.Application;
using XyloType.Application.Interfaces;
using XyloType.Domain.Typing.Analysis;
using XyloType.Factories;

namespace XyloType.Services;

public class MauiNavigationService : INavigationService
{
    private readonly ITypingViewFactory _typingViewFactory;
    private readonly IExerciceGeneratorViewFactory _exerciceViewFactory;
    private readonly IStatisticViewFactory _statisticViewFactory;

    public MauiNavigationService(
        ITypingViewFactory typingViewFactory,
        IExerciceGeneratorViewFactory exerciceViewFactory,
        IStatisticViewFactory statisticViewFactory)
    {
        _typingViewFactory = typingViewFactory;
        _exerciceViewFactory = exerciceViewFactory;
        _statisticViewFactory = statisticViewFactory;
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


    public async Task<Result<bool>> NavigateToUpdateExerciceAsync(Guid exerciceGuid)
    {
        var exerciceGeneratorViewResult =
            await _exerciceViewFactory.CreateExerciceUpdaterView(exerciceGuid);

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


    public async Task<Result<bool>> NavigateToStatisticAsync(Dictionary<char, CharStats> stat)
    {
        Result<ContentPage> viewCReationResult = await _statisticViewFactory.Create(stat);

        if(!viewCReationResult.Success)
        {
            return Result<bool>
                .Fail(viewCReationResult.Error);
        }

        await Shell.Current.Navigation.PushAsync(viewCReationResult.GetValue);

        return Result<bool>.Ok(true);
    }


    public async Task PopBackAsync()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
