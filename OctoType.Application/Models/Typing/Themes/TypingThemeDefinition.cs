namespace OctoType.Application.Models.Typing.Themes;

public class TypingThemeDefinition
{
    public string Name { get; set; } = string.Empty;

    public TypingStyle Pending { get; set; } = new();

    public TypingStyle Current { get; set; } = new();

    public TypingStyle Correct { get; set; } = new();

    public TypingStyle CorrectWithError { get; set; } = new();

    public TypingStyle CurrentWrong { get; set; } = new();
}
