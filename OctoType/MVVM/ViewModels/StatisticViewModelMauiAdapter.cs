using CommunityToolkit.Mvvm.ComponentModel;

using Microcharts;

using OctoType.Application.Interfaces;
using OctoType.Application.Models.Themes;
using OctoType.Domain.Typing.Analysis;
using OctoType.ViewModels.Statistic;

using SkiaSharp;

namespace OctoType.MVVM.ViewModels;


public partial class StatisticViewModelMauiAdapter : ObservableObject
{
    private StatisticViewModel _statisticViewModel;

    public BarChart TheChart { get; set; }

    private readonly IChartResponseTimeColorsProvider _chartResponseTimeColorsProvider;
    private readonly ThemeState _themeState;

    public StatisticViewModelMauiAdapter(
        StatisticViewModel statisticViewModel,
        IChartResponseTimeColorsProvider chartResponseTimeColorsProvider,
        ThemeState themeState)
    {
        _statisticViewModel = statisticViewModel;
        _chartResponseTimeColorsProvider = chartResponseTimeColorsProvider;
        _themeState = themeState;
    }

    public int TotalOccurence { get; set; } = 0;
    public double TotalMinute { get; set; } = 0.0;


    public double LettersPerMinute => TotalMinute > 0 
                                        ? TotalOccurence / TotalMinute
                                        : 0.0;

    public double WordsPerMinute => LettersPerMinute / 5.0;

    public string LetterPerMinuteText => $"{LettersPerMinute:F2}";
    public string WordsPerMinuteText => $"{WordsPerMinute:F2}";


    public void Init()
    {
        List<ChartEntry> gatherResponseTime = [];

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

            SKColor.Parse(_chartResponseTimeColorsProvider.GetHexColorTimeResponse(timeResponseAverage, _themeState));

            var entry = new ChartEntry((float)timeResponseAverage)
            {
                Label = item.Key.ToString(),
                ValueLabel = $"{timeResponseAverage:f2}",
                Color = colorLabel,
                ValueLabelColor = colorText,
                TextColor = colorText,
            };

            gatherResponseTime.Add(entry);
        }


        SKColor colorBg = SKColor.Parse(_chartResponseTimeColorsProvider.GetHexColorBg(_themeState));

        TheChart = new BarChart()
        {
            Entries = [.. gatherResponseTime.OrderByDescending(x => x.Value)],
            MinValue = 0,
            MaxValue = 5,
            LabelOrientation = Orientation.Horizontal,
            BackgroundColor = colorBg,
        };
    }

}
