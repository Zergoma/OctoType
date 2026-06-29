namespace OctoType.Application.Interfaces;

public interface INavigationService
{
    Task<Result<bool>> NavigateToTypingExerciseAsync(IStringsProvider stringProvider);

    Task<Result<bool>> NavigateToExerciceGeneratorAsync();

    Task PopBackAsync();
}