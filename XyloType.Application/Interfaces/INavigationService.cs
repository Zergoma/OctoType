using XyloType.Domain.Typing.Analysis;

namespace XyloType.Application.Interfaces;

public interface INavigationService
{
    Task<Result<bool>> NavigateToTypingExerciseAsync(IStringsProvider stringProvider);

    Task<Result<bool>> NavigateToStatisticAsync(Dictionary<char, CharStats> stat);

    Task<Result<bool>> NavigateToExerciceGeneratorAsync();

    Task<Result<bool>> NavigateToUpdateExerciceAsync(Guid exercice);

    Task PopBackAsync();
}