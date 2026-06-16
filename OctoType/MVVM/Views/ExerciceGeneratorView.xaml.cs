using OctoType.MVVM.ViewModels;

namespace OctoType.MVVM.Views;

public partial class ExerciceGeneratorView : ContentPage
{
	public ExerciceGeneratorView(ExerciceGeneratorViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        if(BindingContext is ExerciceGeneratorViewModel viewModel)
		{
			await viewModel.InitializeAsync();
		}
    }
}