namespace OctoType.Application.Models;

public class TypingExercise
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string? Language { get; set; }

    public TypingExerciseStatic? Static { get; set; }

    public TypingExerciseDynamic? Dynamic { get; set; }
}

