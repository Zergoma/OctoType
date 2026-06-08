using OctoType.MVVM.ViewModels;

namespace OctoType.MVVM.Views;

public partial class ImportBookView : ContentPage
{
	public ImportBookView(ImportBookViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

    }
}