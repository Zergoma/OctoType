using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Themes;
using OctoType.Domain.Typing;

namespace OctoType.Infrastructure.Theme;

public class JsonTypingTheme : ITypingTheme
{
    private readonly TypingThemeDefinition _definition;

    public JsonTypingTheme(
        TypingThemeDefinition definition)
    {
        _definition = definition;
    }

    public TypingStyle GetStyle(TypingCharEnumState state)
    {
        return state switch
        {
            TypingCharEnumState.Pending => _definition.Pending,
            TypingCharEnumState.Current =>_definition.Current,
            TypingCharEnumState.Correct =>_definition.Correct,
            TypingCharEnumState.CorrectWithError =>_definition.CorrectWithError,
            TypingCharEnumState.CurrentWrong =>_definition.CurrentWrong,

            _ => throw new ArgumentOutOfRangeException(nameof(state))
        };
    }
}