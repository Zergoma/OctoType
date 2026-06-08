using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using OctoType.Domain.Enums;
using OctoType.Interfaces;

namespace OctoType.Models.UI.Typing;

public partial class TypingLineState : ObservableObject
{
    private readonly ITypingCharFactory _factoryTypingChar;
    public TypingLineState(
        ITypingCharFactory factoryTypingChar,
        string rawLine)
    {
        _factoryTypingChar = factoryTypingChar;
        RawLine = rawLine;
    }

    public ObservableCollection<TypingCharState> Characters { get; } = [];

    [ObservableProperty]
    public partial bool IsCurrentLine { get; set; }

    [ObservableProperty]
    public partial string RawLine { get; set; } = string.Empty;

    partial void OnRawLineChanged(string value)
    {
        Load(RawLine);
    }

    public void Load(string line)
    {
        Characters.Clear();
        foreach (char c in line)
        {
            Characters.Add(
                _factoryTypingChar.CreateAsync(c, TypingCharEnumState.Pending, "OctoType_Typing_Theme").Result);
        }

        // add enter at the end of the line
        // visual character for "enter"
        Characters.Add(
            _factoryTypingChar.CreateAsync('↵', TypingCharEnumState.Pending, "OctoType_Typing_Theme").Result);
    }
}
