using CommunityToolkit.Mvvm.ComponentModel;

using XyloType.Domain.Typing.Analysis;

namespace XyloType.ViewModels.Statistic;

public partial class StatisticViewModel : ObservableObject
{
    [ObservableProperty]
    public partial Dictionary<char, CharStats> Statistics { get; set; }

    public StatisticViewModel(Dictionary<char, CharStats> stat)
    {
        Statistics = stat;
    }
}
