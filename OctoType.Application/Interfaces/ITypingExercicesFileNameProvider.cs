using OctoType.Application.DTOs;

namespace OctoType.Application.Interfaces
{
    public interface ITypingExercicesFileNameProvider
    {
        Result<string> GetFileName(KeyboardLayoutEnumDto keyboard);
    }
}