using OctoType.Application.Interfaces;

namespace OctoType.Application.Services;

public class TypingExerciseLineNumberService : ITypingExerciseLineNumberService
{
    public int LineNumber { get; set; } = 5;
}