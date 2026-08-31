using XyloType.Application.Interfaces;

namespace XyloType.Application.Services;

public class TypingExerciseLineNumberService : ITypingExerciseLineNumberService
{
    public int LineNumber { get; set; } = 5;
}