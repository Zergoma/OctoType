using CommunityToolkit.Mvvm.ComponentModel;

using OctoType.Domain.Typing;
using OctoType.Interfaces;

namespace OctoType.Models.UI.Typing;


public partial class TypingCharState : ObservableObject
{
    private readonly ITypingTheme _typingTheme;
    public TypingChar Model { get; }

    private TypingStyle Style => _typingTheme.GetStyle(State);


    public TypingCharState(
        ITypingTheme typingTheme,
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
