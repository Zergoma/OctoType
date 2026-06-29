using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Mappers;

namespace OctoType.Infrastructure.Providers;

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
