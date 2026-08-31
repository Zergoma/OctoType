using VM = XyloType.ViewModels.Import;

namespace XyloType.MVVM.Views;

public partial class ImportWordView : ContentPage
{
	public ImportWordView(VM.ImportWordViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}