using OctoType.MVVM.ViewModels;
using OctoType.ViewModels.Statistic;

namespace OctoType.MVVM.Views;

public partial class StatisticView : ContentPage
{
	public StatisticView(StatisticViewModelMauiAdapter vm )
	{
		InitializeComponent();
		BindingContext = vm;
	}
}