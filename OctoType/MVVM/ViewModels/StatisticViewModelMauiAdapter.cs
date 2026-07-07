using CommunityToolkit.Mvvm.ComponentModel;

using Microcharts;

using OctoType.Domain.Typing.Analysis;
using OctoType.ViewModels.Statistic;

using SkiaSharp;

namespace OctoType.MVVM.ViewModels;

public partial class StatisticViewModelMauiAdapter : ObservableObject
{
    private StatisticViewModel _statisticViewModel;

    public BarChart TheChart { get; set; }


    private readonly SKColor[] _colors;

    private static int GetColorIndex(double timeResponse) =>
        timeResponse switch
        {
            < 1.0 => 0,
            < 2.0 => 1,
            < 3.0 => 2,
            < 4.0 => 3,
            _ => 4
        };


    public StatisticViewModelMauiAdapter(StatisticViewModel statisticViewModel)
    {
        _statisticViewModel = statisticViewModel;

        _colors = [
            SKColor.Parse("#42E506"),
            SKColor.Parse("#B5E200"),
            SKColor.Parse("#E07700"),
            SKColor.Parse("#DD2400"),
            SKColor.Parse("#3D0900"),
            ];
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

            var entry = new ChartEntry((float)timeResponseAverage)
            {
                Label = item.Key.ToString(),
                ValueLabel = $"{timeResponseAverage:f2}",
                Color = _colors[GetColorIndex(timeResponseAverage)]
            };

            gatherResponseTime.Add(entry);
        }


        TheChart = new BarChart()
        {
            Entries = [.. gatherResponseTime],
            MinValue = 0,
            MaxValue = 5,
            LabelOrientation = Orientation.Horizontal,

        };
    }

}
