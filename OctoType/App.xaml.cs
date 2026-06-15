using OctoType.MVVM.Views;
using MauiAppNS = Microsoft.Maui.Controls;

namespace OctoType;

public partial class App : MauiAppNS.Application
{
    private readonly IServiceProvider _serviceProvider;
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Window win = new Window(new AppShell());
#if WINDOWS
        win.Width = 800;
        win.Height = 700;

        win.MinimumHeight = 700;
        win.MinimumWidth = 800;
#endif
        return win;
        //return new Window(_serviceProvider.GetRequiredService<TypingView>());
    }
}