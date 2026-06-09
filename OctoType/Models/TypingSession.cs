using System.Collections.ObjectModel;

using OctoType.Domain.Enums;
using OctoType.Models.UI.Typing;

using Windows.Devices.Geolocation;

namespace OctoType.Models;

public class TypingSession
{
    public event Action<int>? LineChanged;

    public ObservableCollection<TypingLineState> Lines { get; } = [];

    public int CurrentLineIndex { get; private set; } = 0;

    public int CurrentCharacterIndex { get; private set; } = 0;

    private void SetPosition(int lineIndex, int characterIndex)
    {
        int lineIdxSecured = Math.Max(lineIndex, 0);
        int charIdxSecured = Math.Max(characterIndex, 0);
        
        bool lineDelta = CurrentLineIndex != lineIdxSecured;
        bool columnDelat = CurrentCharacterIndex != charIdxSecured;

        if (lineDelta)
        { 
            CurrentLineIndex = lineIdxSecured;
            LineChanged?.Invoke(CurrentLineIndex);
        }
        
        if (columnDelat)
        {
            CurrentCharacterIndex = charIdxSecured;
        }

        if(lineDelta || columnDelat)
        {
            UpdateCurrent();
        }
    }

    public void Reset()
    {
        // Reset Character state and error
        for (int i = 0; i < Lines.Count; i++)
        {
            TypingLineState line = Lines[i];

            for (int j = 0; j < line.Characters.Count; j++)
            {
                line.Characters[j].Errors.Clear();
                line.Characters[j].NbError = 0;
                line.Characters[j].IsCurrent = false;
                line.Characters[j].State = TypingCharEnumState.Pending;
            }
        }

        _previousCurrent = null;
        SetPosition(0, 0);
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

        SetPosition(next, 0);

        return true;
    }
    public bool MoveToNextCharacter()
    {
        TypingLineState? line = CurrentLine;

        if (line == null)
        {
            return false;
        }

        int nextChar = CurrentCharacterIndex + 1;

        if (nextChar >= line.Characters.Count)
        {
            return false;
        }

        SetPosition(CurrentLineIndex, nextChar);

        return true;
    }

    public bool CanMoveToPreviousChar()
    {
        if (CurrentCharacterIndex > 0)
            return true;

        return false;
    }

    public bool MoveToPreviousCharacter()
    {
        if (!CanMoveToPreviousChar())
            return false;

        SetPosition(CurrentLineIndex, CurrentCharacterIndex-1);
        return true;
    }

    public bool CanMoveToPreviousLine()
    {
        if (CurrentLineIndex > 0)
            return true;

        return false;
    }

    public bool MoveToPreviousLine()
    {
        if (!CanMoveToPreviousLine())
            return false;

        int prevLineIdx = CurrentLineIndex - 1;
        var prevLine = Lines[prevLineIdx];
        SetPosition(prevLineIdx, prevLine.Characters.Count -1);

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
            if (CanMoveToPreviousChar() || CanMoveToPreviousLine())
            {
                ResetCurrentCharacterTo(TypingCharEnumState.Pending);
            }

            if (MoveToPreviousCharacter() || MoveToPreviousLine())
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
