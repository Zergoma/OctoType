using System.Collections.ObjectModel;

using OctoType.Domain.Enums;
using OctoType.Models.UI.Typing;

namespace OctoType.Models;

public class TypingSession
{
    public event Action<int>? LineChanged;

    public ObservableCollection<TypingLineState> Lines { get; } = [];

    public int CurrentLineIndex { get; private set; } = 0;

    public int CurrentCharacterIndex { get; private set; } = 0;

    public void Reset()
    {
        _previousCurrent = null;
        CurrentLineIndex = 0;
        CurrentCharacterIndex = 0;

        for (int i = 0; i < Lines.Count; i++)
        {
            var line = Lines[i];

            for (int j = 0; j < line.Characters.Count; j++)
            {
                line.Characters[j].Errors.Clear();
                line.Characters[j].NbError = 0;
                line.Characters[j].IsCurrent = false;
                line.Characters[j].State = TypingCharEnumState.Pending;
            }
        }

        var c = CurrentCharacter;
        if (c != null)
        {
            c.State = TypingCharEnumState.Current;
        }

        LineChanged?.Invoke(CurrentLineIndex);
        UpdateCurrent();
    }

    private TypingCharState? _previousCurrent;
    private void UpdateCurrent()
    {
        if (_previousCurrent != null)
        {
            _previousCurrent.IsCurrent = false;

            if (_previousCurrent.State == TypingCharEnumState.Current)
            {
                _previousCurrent.State = TypingCharEnumState.Pending;
            }
        }

        TypingCharState? current = CurrentCharacter;

        if (current != null)
        {
            current.IsCurrent = true;

            if (current.State == TypingCharEnumState.Pending)
            {
                current.State = TypingCharEnumState.Current;
            }
        }

        _previousCurrent = current;
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
        LineChanged?.Invoke(CurrentLineIndex);

        CurrentCharacterIndex = 0;
        UpdateCurrent();

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

        UpdateCurrent();

        return true;
    }

    public bool CanMoveToPrevious()
    {
        if (CurrentCharacterIndex > 0)
            return true;

        return false;
    }

    public bool MoveToPreviousCharacter()
    {
        if (!CanMoveToPrevious())
            return false;

        CurrentCharacterIndex--;
        UpdateCurrent();
        return true;
    }

    private void ResetCurrentCharacterTo(TypingCharEnumState state)
    {
        var c = CurrentCharacter;
        if (c == null)
            return;

        c.State = state;
        c.NbError = 0;
    }

    public TypingStatus ProcessInput(char input, Func<char, char> mapper)
    {
        // BACKSPACE
        if (input == '\b')
        {
            if (CanMoveToPrevious())
            {
                ResetCurrentCharacterTo(TypingCharEnumState.Pending);
            }

            if (MoveToPreviousCharacter())
            {
                ResetCurrentCharacterTo(TypingCharEnumState.Current);
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
