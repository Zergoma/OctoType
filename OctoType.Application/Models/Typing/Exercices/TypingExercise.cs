namespace OctoType.Application.Models.Typing.Exercices;

public class TypingExercise
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;


    public List<TypingExerciseConfiguration> ExerciceConfigs { get; set; } = [];
}

