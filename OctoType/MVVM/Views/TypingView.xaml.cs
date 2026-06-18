using System.Diagnostics;

using Microsoft.UI.Xaml.Input;
using OctoType.Domain.Typing;
using OctoType.ViewModels.Typing;


namespace OctoType.MVVM.Views;

public partial class TypingView : ContentPage
{
	public TypingView(TypingViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

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
    }

    protected override async void OnAppearing()
	{
        base.OnAppearing();

        if (BindingContext is TypingViewModel vm)
		{
            // need to load asset outise the UI thread
            await Task.Run(async () =>
            {
                await vm.LoadTextAsync();
            });
        }

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

        vm.ProcessInput(input);

        HiddenInput.Text = string.Empty;
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
                vm.ProcessInput('\b');
                e.Handled = true;
                break;

            case Windows.System.VirtualKey.Enter:
                vm.ProcessInput('\n');
                e.Handled = true;
                HiddenInput.Text = string.Empty;
                break;

            case Windows.System.VirtualKey.F5:
                vm.Session.ResetProgression();
                e.Handled = true;
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