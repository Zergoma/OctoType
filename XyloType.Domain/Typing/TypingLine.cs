namespace XyloType.Domain.Typing;

public class TypingLine
{
    public List<TypingChar> Characters { get; } = [];

    public TypingLine(string line)
    {
        foreach (char item in line)
        {
            Characters.Add(new TypingChar(item));
        }
    }
}
