using XyloType.Application.Models.Typing.Exercices;

namespace XyloType.Application.Interfaces
{
    public interface ITypingExercicesEngine
    {
        Result<TypingExercise> CurrentExercice();
        Result<TypingExercise> NextExercice();
        Result<TypingExercise> PreviousExercice();
        Result<bool> SetIdx(int idx);
    }
}