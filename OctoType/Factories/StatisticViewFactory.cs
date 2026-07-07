using OctoType.Application;
using OctoType.Domain.Typing.Analysis;
using OctoType.MVVM.ViewModels;
using OctoType.MVVM.Views;
using OctoType.ViewModels.Statistic;

namespace OctoType.Factories;

public class StatisticViewFactory : IStatisticViewFactory
{
    public async Task<Result<ContentPage>> Create(Dictionary<char, CharStats> stat)
    {
        StatisticViewModel vm = new(stat);

        StatisticViewModelMauiAdapter vmadapter = new(vm);
        vmadapter.Init();

        StatisticView vieww = new(vmadapter);

        return Result<ContentPage>.Ok(vieww);
    }
}
