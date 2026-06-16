using VM = OctoType.ViewModels.Import;

namespace OctoType.MVVM.Views;

public partial class ImportWordView : ContentPage
{
	public ImportWordView(VM.ImportWordViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}