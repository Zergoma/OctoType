using OctoType.Application.Models;
using OctoType.Application.UseCases;

namespace OctoType.Application.Interfaces;

public interface ITypingExerciceSettingFactory
{
    TypingExercise GenerateStaticTypingExercices(
        TypingExerciseCreateParameters  typingExerciceSetting,
        string generatedText);

    TypingExercise GenerateDynamicTypingExercices(TypingExerciseCreateParameters typingExerciceSetting);
}