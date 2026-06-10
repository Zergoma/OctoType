using OctoType.MVVM.ViewModels;

namespace OctoType.MVVM.Views;

public partial class ExerciceGeneratorView : ContentPage
{
	public ExerciceGeneratorView(ExerciceGeneratorViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}