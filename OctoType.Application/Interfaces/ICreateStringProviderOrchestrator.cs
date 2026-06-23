using OctoType.Application.DTOs;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.Interfaces
{
    public interface ICreateStringProviderOrchestrator
    {
        Result<IStringsProvider> Create(TypingExercise exercice, KeyBoardLayoutDto selectedKeyboard);
    }
}