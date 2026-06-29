using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.ViewModels.TypingLauncher;

namespace OctoType.MVVM.Views;

public partial class TypingLauncherView : ContentPage
{
	private readonly IUserKeyboardLayoutPreferenceService _userKeyboardPreferenceService;

    public TypingLauncherView(
        TypingLauncherViewModel vm,
        IUserKeyboardLayoutPreferenceService userKeyboardPreferenceService)
    {
        InitializeComponent();
        _userKeyboardPreferenceService = userKeyboardPreferenceService;
        BindingContext = vm;

        // Update user preference
        vm.KeyboardLayoutChanged += OnKeyboardChanged;
    }

    private async Task OnKeyboardChanged(int keyboardId)
    {
        _userKeyboardPreferenceService.SetKeyboardType(keyboardId);

        if (BindingContext is TypingLauncherViewModel vm)
        {
            await vm.InitilizationAsync(keyboardId);
        }
    }

    protected async override void OnAppearing()
    {
        if(BindingContext is TypingLauncherViewModel vm)
		{
            Result<int> keyboardCodeResult = _userKeyboardPreferenceService.GetKeyboardType();
            if(!keyboardCodeResult.Success)
            {
                return;
            }

            await vm.InitilizationAsync(keyboardCodeResult.GetValue);
		}
    }
}