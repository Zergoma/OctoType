using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using OctoType.Domain.Enums;
using OctoType.Interfaces;

namespace OctoType.Models.UI.Typing;


public partial class TypingCharState : ObservableObject
{
    private readonly ITypingTheme _typingTheme;
    private TypingStyle Style => _typingTheme.GetStyle(State);


    public TypingCharState(
        ITypingTheme typingTheme)
    {
        _typingTheme = typingTheme;
    }

    [ObservableProperty]
    public partial char Character { get; set; }

    [ObservableProperty]
    public partial TypingCharEnumState State { get; set; }

    [ObservableProperty]
    public partial bool IsCurrent { get; set; }

    partial void OnIsCurrentChanged(bool oldValue, bool newValue)
    {
        if (!newValue)
        {
            return;
        }

        if (State == TypingCharEnumState.Pending)
        {
            State = TypingCharEnumState.Current;
        }
    }

    [ObservableProperty]
    public partial int NbError { get; set; } = 0;


    public ObservableCollection<char> Errors { get; set; } = [];

    public Color TextColor => Style.GetTextColor();
    public Color BgColor => Style.GetBackgroundColor();
    public Color BorderColor => Style.GetBorderColor();
    public int BorderThikness => Style.BorderThickness;

    partial void OnStateChanged(TypingCharEnumState value)
    {
        OnPropertyChanged(nameof(TextColor));
        OnPropertyChanged(nameof(BgColor));
        OnPropertyChanged(nameof(BorderColor));
        OnPropertyChanged(nameof(BorderThikness));
    }

    public bool ChallengeValue(char input)
    {
        if (input == Character)
        {
            State = NbError switch
            {
                0 => TypingCharEnumState.Correct,
                _ => TypingCharEnumState.CorrectWithError,
            };
            return true;
        }
        
        State = TypingCharEnumState.CurrentWrong;
        Errors.Add(input);
        NbError++;
        return false;
    }
}
