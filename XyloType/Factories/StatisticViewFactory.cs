using Microsoft.Extensions.Logging;

using XyloType.MVVM.ViewModels;
using XyloType.MVVM.Views;
using XyloType.ViewModels.Statistic;

using XyloType.Application;
using XyloType.Application.Interfaces;
using XyloType.Application.Models.Themes;
using XyloType.Domain.Typing.Analysis;

namespace XyloType.Factories;

public class StatisticViewFactory : IStatisticViewFactory
{
    private readonly IThemeChangerService _themeChangerService;
    private readonly IChartResponseTimeColorsProvider _chartResponseTimeColorsProvider;
    private readonly IChartErrorProvider _chartErrorColorsProvider;
    private readonly ILogger<StatisticViewModelMauiAdapter> _logger;

    public StatisticViewFactory(
        IThemeChangerService themeChangerService,
        IChartResponseTimeColorsProvider chartResponseTimeColorsProvider,
        IChartErrorProvider chartErrorColorsProvider,
        ILogger<StatisticViewModelMauiAdapter> logger)
    {
        _themeChangerService = themeChangerService;
        _chartResponseTimeColorsProvider = chartResponseTimeColorsProvider;
        _chartErrorColorsProvider = chartErrorColorsProvider;
        _logger = logger;
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
                themeState,
                _logger);
        
        vmadapter.Init();

        StatisticView vieww = new(vmadapter);

        return Result<ContentPage>.Ok(vieww);
    }
}
