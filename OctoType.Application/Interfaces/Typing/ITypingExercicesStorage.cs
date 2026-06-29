using OctoType.Application.DTOs;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Application.Interfaces.Typing;

public interface ITypingExercicesStorage
{
    Task<Result<TypingExercices>> LoadAsync(KeyboardLayoutEnumDto keyboard);
    Task<Result<bool>> SaveAsync(TypingExercices? exercices);
}