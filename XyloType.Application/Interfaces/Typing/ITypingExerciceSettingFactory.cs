using XyloType.Application.Models.Typing.Exercices;
using XyloType.Application.UseCases;

namespace XyloType.Application.Interfaces.Typing;

public interface ITypingExerciceSettingFactory
{
    TypingExercise GenerateStaticTypingExercices(
        TypingExerciseCreateParameters  typingExerciceSetting,
        string generatedText);

    TypingExercise GenerateDynamicTypingExercices(
        TypingExerciseCreateParameters typingExerciceSetting,
        TypingTextDataDynamic dynamicTypingTextData);
}