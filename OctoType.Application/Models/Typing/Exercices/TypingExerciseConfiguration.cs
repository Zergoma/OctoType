using OctoType.Application.DTOs;

namespace OctoType.Application.Models.Typing.Exercices;

public class TypingExerciseConfiguration
{
    public KeyBoardLayoutDto KeyboardLayout { get; set; }
    public string AllowedLetters { get; set; } = string.Empty;
}

