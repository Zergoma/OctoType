using XyloType.Application.DTOs;

namespace XyloType.Application.UseCases;

public class TypingExerciseCreateParameters
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }  = string.Empty;
    public string? Language { get; set; }
    public required KeyBoardLayoutDto KeyBoardLayoutDto { get; set; }
    public string AllowedLetters { get; set; }  = string.Empty;
}