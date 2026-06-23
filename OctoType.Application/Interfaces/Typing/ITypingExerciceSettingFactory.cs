using OctoType.Application.Models.Typing.Exercices;
using OctoType.Application.UseCases;

namespace OctoType.Application.Interfaces.Typing;

public interface ITypingExerciceSettingFactory
{
    TypingExercise GenerateStaticTypingExercices(
        TypingExerciseCreateParameters  typingExerciceSetting,
        string generatedText);

    TypingExercise GenerateDynamicTypingExercices(
        TypingExerciseCreateParameters typingExerciceSetting,
        TypingTextDataDynamic dynamicTypingTextData);
}