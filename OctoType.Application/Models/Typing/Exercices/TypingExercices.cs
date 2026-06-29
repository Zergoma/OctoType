using OctoType.Application.DTOs;

namespace OctoType.Application.Models.Typing.Exercices;

public class TypingExercices
{
    public required KeyBoardLayoutDto KeyboardLayout { get; set; }
    public List<TypingExercise> Exercices { get; set; } = [];
}

