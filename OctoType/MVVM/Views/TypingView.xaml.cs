using System.Diagnostics;

using Microsoft.UI.Xaml.Input;

using OctoType.Application.Interfaces;
using OctoType.Domain.Typing;
using OctoType.ViewModels.Typing;


namespace OctoType.MVVM.Views;

public partial class TypingView : ContentPage
{
    private event Action TextEnded;
    private readonly INavigationService _navigationService;
    private TypingStatus Status
    {
        get;
        set
        {
            field = value;
            if(field == TypingStatus.Ended)
            {
                TextEnded?.Invoke();
            }
        }
    }

    

    public TypingView(
        TypingViewModel vm,
        INavigationService navigationService)
    {
        InitializeComponent();
        BindingContext = vm;

        TextEnded += OnTextEndDetected;

        #region Keyboard Focus

        HiddenInput.Focused += (_, __) =>
        {
            TakeFocusButton.IsVisible = false;
            Debug.WriteLine("FOCUSED");
        };

        HiddenInput.Unfocused += (_, __) =>
        {
            TakeFocusButton.IsVisible = true;
            Debug.WriteLine("UNFOCUSED");
        };
        #endregion

        vm.LineChanged += (int lineNumber) =>
        {
            Dispatcher.DispatchAsync(async () =>
            {
                await Task.Delay(50);

                ScrollToCurrentLine(lineNumber);
            });
        };
        _navigationService = navigationService;
    }

    private async void OnTextEndDetected()
    {
        await _navigationService.PopBackAsync();
    }

    protected override async void OnAppearing()
	{
        base.OnAppearing();

        await Dispatcher.DispatchAsync(async () =>
        {
            HiddenInput.Focus();
        });
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is not TypingViewModel vm)
        {
            return;
        }

        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            return;
        }
        
        char input = e.NewTextValue[^1];
        HiddenInput.Text = string.Empty;

        Status = vm.ProcessInput(input);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (HiddenInput.Handler?.PlatformView is not Microsoft.UI.Xaml.Controls.TextBox nativeTextBox)
            return;

        nativeTextBox.KeyDown += OnNativeKeyDown; ;

        Dispatcher.Dispatch(() =>
        {
            HiddenInput.Focus();
        });
    }

    private void OnNativeKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (BindingContext is not TypingViewModel vm)
            return;

        var key = e.Key;

        switch (key)
        {
            case Windows.System.VirtualKey.Back:
                e.Handled = true;
                Status = vm.ProcessInput('\b');
                break;

            case Windows.System.VirtualKey.Enter:
                e.Handled = true;
                HiddenInput.Text = string.Empty;
                Status = vm.ProcessInput('\n');
                break;

            case Windows.System.VirtualKey.F5:
                e.Handled = true;
                vm.Session.ResetProgression();
                break;
        }
    }

    private void TakeFocusButton_Clicked(object sender, EventArgs e)
    {
        HiddenInput.Focus();
    }

    public void ScrollToCurrentLine(int index)
    {
        if(index <0 )
            return;

        TypingCollectionView.ScrollTo(
            index,
            position: ScrollToPosition.Start,
            animate: true);
    }
}