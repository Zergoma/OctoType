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
        return new Window(new AppShell());
        //return new Window(_serviceProvider.GetRequiredService<TypingView>());
    }
}