using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Exercices;
using OctoType.Application.UseCases;

namespace OctoType.Application.Interfaces
{
    public interface ISaveTypingExerciceUseCase
    {
        Task<Result<bool>> SaveNewExerciceAsync(
            TypingExerciseCreateParameters parameters,
            bool isStatic,
            string? generatedText,
            ITypingExercicesManager exercicesManager,
            TypingTextDataDynamic? typingTextData);

        Task<Result<bool>> UpdateExerciceAsync(
           ITypingExercicesManager exerciceManager,
           TypingExercise exercice);
    }
}