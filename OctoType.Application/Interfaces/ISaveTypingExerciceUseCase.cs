using OctoType.Application.Interfaces.Typing;
using OctoType.Application.UseCases;

namespace OctoType.Application.Interfaces
{
    public interface ISaveTypingExerciceUseCase
    {
        Task<Result<bool>> ExecuteAsync(
            TypingExerciseCreateParameters parameters,
            bool isStatic,
            string? generatedText,
            ITypingExercicesManager exercicesManager);
    }
}