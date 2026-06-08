using System.Diagnostics;

using OctoType.Domain.Enums;
using OctoType.Models.UI.Typing;
using OctoType.MVVM.ViewModels;

namespace OctoType.MVVM.Views;

public partial class TypingView : ContentPage
{
	public TypingView(TypingViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

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

        vm.CurrentLineChanged += 
            (currentLine) =>
            {
                Dispatcher.DispatchAsync(async () =>
                {
                    await Task.Delay(200);

                    ScrollToCurrentLine(currentLine);
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
            //await vm.InitializeAsync();
        }

        await Dispatcher.DispatchAsync(async () =>
        {
            HiddenInput.Focus();
        });
    }

    private void OnCompleted(object sender, EventArgs e)
    {
        if (BindingContext is not TypingViewModel vm)
        {
            return;
        }

        // articicialy send new line on enter
        vm.ProcessInput('\n');

        HiddenInput.Text = string.Empty;
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

        TypingStatus typingStatus = vm.ProcessInput(input);

        HiddenInput.Text = string.Empty;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler != null)
        {
            Dispatcher.Dispatch(() =>
            {
                HiddenInput.Focus();
            });
        }
    }

    private void TakeFocusButton_Clicked(object sender, EventArgs e)
    {
        HiddenInput.Focus();
    }

    public void ScrollToCurrentLine(TypingLineState? currentLine)
    {
        if (currentLine == null)
            return;

        TypingCollectionView.ScrollTo(
            currentLine,
            position: ScrollToPosition.Start,
            animate: true);
    }
}