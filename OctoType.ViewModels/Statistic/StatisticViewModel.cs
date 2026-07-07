using CommunityToolkit.Mvvm.ComponentModel;

using OctoType.Application;
using OctoType.Domain.Typing.Analysis;

namespace OctoType.ViewModels.Statistic;

public partial class StatisticViewModel : ObservableObject
{
    [ObservableProperty]
    public partial Dictionary<char, CharStats> Statistics { get; set; }

    public StatisticViewModel(Dictionary<char, CharStats> stat)
    {
        Statistics = stat;
    }
}
