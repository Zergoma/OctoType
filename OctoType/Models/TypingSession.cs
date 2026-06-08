using System.Collections.ObjectModel;

using OctoType.Domain.Enums;
using OctoType.Models.UI.Typing;

namespace OctoType.Models;

public class TypingSession
{
    public event Action? StateChanged;
    public event Action? LineChanged;
    public ObservableCollection<TypingLineState> Lines { get; } = [];

    public int CurrentLineIndex { get; private set; }

    public int CurrentCharacterIndex { get; private set; }

    public void Reset()
    {
        CurrentLineIndex = 0;
        CurrentCharacterIndex = 0;
        LineChanged?.Invoke();
        StateChanged?.Invoke();
    }

    public TypingLineState? CurrentLine
    {
        get
        {
            if (CurrentLineIndex < 0 ||
                CurrentLineIndex >= Lines.Count)
            {
                return null;
            }

            return Lines[CurrentLineIndex];
        }
    }

    public TypingCharState? CurrentCharacter
    {
        get
        {
            TypingLineState? line = CurrentLine;

            if (line == null)
            {
                return null;
            }

            if (CurrentCharacterIndex < 0 ||
                CurrentCharacterIndex >= line.Characters.Count)
            {
                return null;
            }

            return line.Characters[CurrentCharacterIndex];
        }
    }


    public bool MoveToNextLine()
    {
        int next = CurrentLineIndex + 1;

        if (next >= Lines.Count)
        {
            return false;
        }

        CurrentLineIndex = next;
        LineChanged?.Invoke();

        CurrentCharacterIndex = 0;
        StateChanged?.Invoke();

        return true;
    }
    public bool MoveToNextCharacter()
    {
        TypingLineState? line = CurrentLine;

        if (line == null)
        {
            return false;
        }

        int next = CurrentCharacterIndex + 1;

        if (next >= line.Characters.Count)
        {
            return false;
        }

        CurrentCharacterIndex = next;

        StateChanged?.Invoke();
        return true;
    }

    public bool MoveToPreviousCharacter()
    {
        if (CurrentCharacterIndex > 0)
        {
            CurrentCharacterIndex--;
            StateChanged?.Invoke();
            return true;
        }

        return false;
    }

    private void ResetCurrentCharacter()
    {
        var c = CurrentCharacter;
        if (c == null)
            return;

        c.State = TypingCharEnumState.Pending;
        //c.NbError = 0;
    }

    public TypingStatus ProcessInput(char input, Func<char, char> mapper)
    {
        // BACKSPACE
        if (input == '\b')
        {
            if (MoveToPreviousCharacter())
            {
                ResetCurrentCharacter();
                return TypingStatus.InProgress;
            }

            return TypingStatus.InProgress;
        }

        TypingCharState? current = CurrentCharacter;

        if (current == null)
        {
            return TypingStatus.Ended;
        }

        bool success = current.ChallengeValue(mapper(input));

        if (success)
        {
            if (!MoveToNextCharacter())
            {
                if (!MoveToNextLine())
                {
                    return TypingStatus.Ended;
                }
            }
        }

        return TypingStatus.InProgress;
    }

}
