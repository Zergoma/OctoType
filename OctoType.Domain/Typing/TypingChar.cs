namespace OctoType.Domain.Typing;

public class TypingChar
{
    public event Action? StateChanged;

    public TypingChar(char c)
    {
        Character = c;
    }

    public char Character { get; set; }

    private TypingCharEnumState _state;
    public TypingCharEnumState State
    {
        get => _state; 
        set
        {
            if (_state == value)
                return;
            _state = value;
            StateChanged?.Invoke();
        }
    }

    public int NbError { get; set; }

    public List<char> Errors { get; } = [];

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

    public void Reset(bool resetAll = true)
    {
        if (resetAll)
        {
            Errors.Clear();
        }
        NbError = 0;
        State = TypingCharEnumState.Pending;
    }
}