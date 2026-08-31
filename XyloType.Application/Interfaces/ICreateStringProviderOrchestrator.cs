using XyloType.Application;
using XyloType.Application.DTOs;
using XyloType.Application.Models.Typing.Exercices;

namespace XyloType.Application.Interfaces
{
    public interface ICreateStringProviderOrchestrator
    {
        Result<IStringsProvider> Create(TypingExercise exercice, KeyBoardLayoutDto selectedKeyboard);
    }
}