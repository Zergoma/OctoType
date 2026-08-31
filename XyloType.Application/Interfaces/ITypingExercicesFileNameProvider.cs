using XyloType.Application;
using XyloType.Application.DTOs;

namespace XyloType.Application.Interfaces
{
    public interface ITypingExercicesFileNameProvider
    {
        Result<string> GetFileName(KeyboardLayoutEnumDto keyboard);
    }
}