using VM = OctoType.ViewModels.Exercices;

namespace OctoType.MVVM.Views;

public partial class ExerciceGeneratorView : ContentPage
{
	public ExerciceGeneratorView(VM.ExerciceGeneratorViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        if(BindingContext is VM.ExerciceGeneratorViewModel viewModel)
		{
			await viewModel.InitializeAsync();
		}
    }
}