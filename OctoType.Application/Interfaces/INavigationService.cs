using OctoType.Domain.Typing.Analysis;

namespace OctoType.Application.Interfaces;

public interface INavigationService
{
    Task<Result<bool>> NavigateToTypingExerciseAsync(IStringsProvider stringProvider);

    Task<Result<bool>> NavigateToStatisticAsync(Dictionary<char, CharStats> stat);

    Task<Result<bool>> NavigateToExerciceGeneratorAsync();

    Task PopBackAsync();
}