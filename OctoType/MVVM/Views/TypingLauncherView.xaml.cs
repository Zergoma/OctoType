using OctoType.ViewModels.TypingLauncher;

namespace OctoType.MVVM.Views;

public partial class TypingLauncherView : ContentPage
{
	public TypingLauncherView(TypingLauncherViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected async override void OnAppearing()
    {
        if(BindingContext is TypingLauncherViewModel vm)
		{
			await vm.Initilization();
		}
    }
}