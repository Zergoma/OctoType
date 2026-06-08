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
    public partial TypingCharState? Current { get; set; }

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

    public void EndLine()
    {
        IsCurrentLine = false;
        if (Current != null)
        {
            Current.IsCurrent = false;
        }
    }

    public void StartLine()
    {
        Current = Characters.First();
        Current.IsCurrent = true;
        IsCurrentLine = true;
    }
    public bool MoveToNextCharacter()
    {
        Current?.IsCurrent = false;

        TypingCharState? next = GetNextCharacter(Current);

        if (next == null)
            return false;

        Current = next;
        next.IsCurrent = true;

        return true;
    }

    private TypingCharState? GetNextCharacter(TypingCharState? current)
    {
        if (current == null)
            return null;

        int index = Characters.IndexOf(current);

        if (index < 0)
            return null;

        int nextIdx = index + 1;
        if (nextIdx >= Characters.Count)
        {
            return null;
        }
        TypingCharState nextChar = Characters[nextIdx];

        return nextChar;
    }

}
