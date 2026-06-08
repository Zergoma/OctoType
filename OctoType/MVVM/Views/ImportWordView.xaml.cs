using OctoType.MVVM.ViewModels;

namespace OctoType.MVVM.Views;

public partial class ImportWordView : ContentPage
{
	public ImportWordView(ImportWordViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}