using XyloType.Application;
using XyloType.Application.Interfaces.Typing;
using XyloType.Application.Models.Typing.Exercices;
using XyloType.Application.UseCases;

namespace XyloType.Application.Interfaces
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