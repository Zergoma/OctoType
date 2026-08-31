using XyloType.Application;
using XyloType.Application.DTOs;
using XyloType.Application.Interfaces;
using XyloType.Application.Mappers;

namespace XyloType.Infrastructure.Providers;

public class PbTypingExercicesFileNameProvider : ITypingExercicesFileNameProvider
{
    public Result<string> GetFileName(KeyboardLayoutEnumDto keyboard)
    {
        Result<string> keyboardHumanResult = keyboard.ToHumanFriendly();

        if (!keyboardHumanResult.Success)
            return Result<string>.Fail(keyboardHumanResult.Error);

        return Result<string>
            .Ok($"Exercices{keyboardHumanResult.GetValue}.pb");
    }
}
