using CommunityToolkit.Mvvm.ComponentModel;

using AppInterfacesTyping = OctoType.Application.Interfaces.Typing;
using AppModelsTyping = OctoType.Application.Models.Typing.Themes;
using OctoType.Domain.Typing;

namespace OctoType.ViewModels.Typing;


public partial class TypingCharStateViewModel : ObservableObject
{
    private readonly AppInterfacesTyping.ITypingTheme _typingTheme;
    public TypingChar Model { get; }

    private AppModelsTyping.TypingStyle Style => _typingTheme.GetStyle(State);


    public TypingCharStateViewModel(
        AppInterfacesTyping.ITypingTheme typingTheme,
        TypingChar model)
    {
        _typingTheme = typingTheme;
        Model = model;

        // All the magic is here
        // model state changed -> trigger property bound to UI
        Model.StateChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        OnPropertyChanged(nameof(State));
        OnPropertyChanged(nameof(TextColor));
        OnPropertyChanged(nameof(BgColor));
        OnPropertyChanged(nameof(BorderColor));
        OnPropertyChanged(nameof(BorderThikness));
    }

    public char Character => Model.Character;
    public TypingCharEnumState State => Model.State;

    public string TextColor => Style.TextColor;
    public string BgColor => Style.BackgroundColor;
    public string BorderColor => Style.BorderColor;
    public int BorderThikness => Style.BorderThickness;

}
