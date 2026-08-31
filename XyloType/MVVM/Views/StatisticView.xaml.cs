using XyloType.MVVM.ViewModels;
using XyloType.ViewModels.Statistic;

namespace XyloType.MVVM.Views;

public partial class StatisticView : ContentPage
{
	public StatisticView(StatisticViewModelMauiAdapter vm )
	{
		InitializeComponent();
		BindingContext = vm;
	}
}