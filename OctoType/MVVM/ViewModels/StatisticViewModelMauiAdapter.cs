using CommunityToolkit.Mvvm.ComponentModel;

using Microcharts;

using Microsoft.Extensions.Logging;

using OctoType.Application.Interfaces;
using OctoType.Application.Models.Themes;
using OctoType.Domain.Typing.Analysis;
using OctoType.ViewModels.Statistic;

using SkiaSharp;

namespace OctoType.MVVM.ViewModels;


public partial class StatisticViewModelMauiAdapter : ObservableObject
{
    private readonly StatisticViewModel _statisticViewModel;

    [ObservableProperty]
    public partial BarChart TimeResponseChart { get; set; }

    [ObservableProperty]
    public partial RadarChart ErrorsChart { get; set; }

    private readonly IChartResponseTimeColorsProvider _chartResponseTimeColorsProvider;
    private readonly IChartErrorProvider _chartErrorColorsProvider;
    private readonly ThemeState _themeState;
    private readonly ILogger<StatisticViewModelMauiAdapter> _logger;

    public StatisticViewModelMauiAdapter(
        StatisticViewModel statisticViewModel,
        IChartResponseTimeColorsProvider chartResponseTimeColorsProvider,
        IChartErrorProvider chartErrorColorsProvider,
        ThemeState themeState,
        ILogger<StatisticViewModelMauiAdapter> logger)
    {
        _statisticViewModel = statisticViewModel;
        _chartResponseTimeColorsProvider = chartResponseTimeColorsProvider;
        _themeState = themeState;
        _chartErrorColorsProvider = chartErrorColorsProvider;
        _logger = logger;
    }

    public int TotalOccurence { get; set; } = 0;
    public double TotalMinute { get; set; } = 0.0;


    public double LettersPerMinute => TotalMinute > 0
                                        ? TotalOccurence / TotalMinute
                                        : 0.0;

    public double WordsPerMinute => LettersPerMinute / 5.0;

    public string LetterPerMinuteText => $"{LettersPerMinute:F2}";
    public string WordsPerMinuteText => $"{WordsPerMinute:F2}";

    [ObservableProperty]
    public partial bool HasError { get; set; } = false;

    public void Init()
    {
        List<ChartEntry> gatherResponseTime = [];
        List<ChartEntry> gatherError = [];

        foreach (KeyValuePair<char, CharStats> item in _statisticViewModel.Statistics)
        {
            CharStats charStats = item.Value;

            TotalOccurence += charStats.NbOccurence;
            TotalMinute += charStats.RespondeTime.TotalMinutes;

            // We concidere 5sec as the maximum time to press the key
            // This to avoid to have chart useless because of a pause
            double timeResponseAverage = Math.Min(charStats.ResponseTimeAverage.TotalSeconds, 5.0);

            SKColor colorLabel = SKColor.Parse(_chartResponseTimeColorsProvider.GetHexColorTimeResponse(timeResponseAverage, _themeState));
            SKColor colorText = SKColor.Parse(_chartResponseTimeColorsProvider.GetHexColorTxtLabel(_themeState));

            var timeResponseEntry =
                new ChartEntry((float)timeResponseAverage)
                {
                    Label = item.Key.ToString(),
                    ValueLabel = $"{timeResponseAverage:f2}",
                    Color = colorLabel,
                    ValueLabelColor = colorText,
                    TextColor = colorText,
                };

            gatherResponseTime.Add(timeResponseEntry);


            if (charStats.NbCharError > 0)
            {
                double errorPercentage = 
                    charStats.NbOccurence > 0
                    ? charStats.NbCharError*100.0 / charStats.NbOccurence
                    : 100.0;

                SKColor colorError = SKColor.Parse(_chartErrorColorsProvider.GetHexColorError(errorPercentage, _themeState));

                var errorEntry =
                    new ChartEntry((float)errorPercentage)
                    {
                        Label = item.Key.ToString(),
                        ValueLabel = $"{charStats.NbCharError}/{charStats.NbOccurence} ({errorPercentage:f2})%",
                        Color = colorError,
                        ValueLabelColor = colorText,
                        TextColor = colorText,
                    };
                gatherError.Add(errorEntry);
            }

        }


        SKColor colorBg = SKColor.Parse(_chartResponseTimeColorsProvider.GetHexColorBg(_themeState));

        TimeResponseChart =
            new BarChart()
            {
                Entries = [.. gatherResponseTime.OrderByDescending(x => x.Value)],
                MinValue = 0,
                //MaxValue = 5,
                LabelOrientation = Orientation.Horizontal,
                BackgroundColor = colorBg,
                CornerRadius = 5,
            };

        HasError = gatherError.Count > 0;

        ErrorsChart =
            new RadarChart()
            {
                Entries = [.. gatherError.OrderByDescending(x => x.Value)],
                MinValue = 0,
                LabelTextSize = 10,
                BackgroundColor = colorBg,
            };


        _logger.LogInformation(
            "Letters per minute {LPM}, Words per minute {WPM}, Errors {CharsError}",
            LettersPerMinute,
            WordsPerMinute,
            _statisticViewModel.Statistics
            .Where(x => x.Value.RealErrors.Count  >0 )
            .Select(x => (
                Character :x.Key, 
                Count : x.Value.RealErrors.Count,
                Errors : string.Join(null, x.Value.RealErrors)
            )));
    }

}
