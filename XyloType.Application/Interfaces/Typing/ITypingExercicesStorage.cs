using XyloType.Application;
using XyloType.Application.DTOs;
using XyloType.Application.Models.Typing.Exercices;

namespace XyloType.Application.Interfaces.Typing;

public interface ITypingExercicesStorage
{
    Task<Result<TypingExercices>> LoadAsync(KeyboardLayoutEnumDto keyboard);
    Task<Result<bool>> SaveAsync(TypingExercices? exercices);
}