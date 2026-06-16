using VM = OctoType.ViewModels.Import;

namespace OctoType.MVVM.Views;

public partial class ImportBookView : ContentPage
{
	public ImportBookView(VM.ImportBookViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

    }
}