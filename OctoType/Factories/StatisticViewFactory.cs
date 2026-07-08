using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Application.Models.Themes;
using OctoType.Domain.Typing.Analysis;
using OctoType.MVVM.ViewModels;
using OctoType.MVVM.Views;
using OctoType.ViewModels.Statistic;

namespace OctoType.Factories;

public class StatisticViewFactory : IStatisticViewFactory
{
    private readonly IThemeChangerService _themeChangerService;
    private readonly IChartResponseTimeColorsProvider _chartResponseTimeColorsProvider;
    private readonly IChartErrorProvider _chartErrorColorsProvider;

    public StatisticViewFactory(
        IThemeChangerService themeChangerService,
        IChartResponseTimeColorsProvider chartResponseTimeColorsProvider,
        IChartErrorProvider chartErrorColorsProvider)
    {
        _themeChangerService = themeChangerService;
        _chartResponseTimeColorsProvider = chartResponseTimeColorsProvider;
        _chartErrorColorsProvider = chartErrorColorsProvider;
    }

    public async Task<Result<ContentPage>> Create(Dictionary<char, CharStats> stat)
    {
        // Get current theme apply
        ThemeState themeState = _themeChangerService.GetTheme();


        StatisticViewModel vm = new(stat);

        StatisticViewModelMauiAdapter vmadapter =
            new(
                vm,
                _chartResponseTimeColorsProvider,
                _chartErrorColorsProvider,
                themeState);
        
        vmadapter.Init();

        StatisticView vieww = new(vmadapter);

        return Result<ContentPage>.Ok(vieww);
    }
}
