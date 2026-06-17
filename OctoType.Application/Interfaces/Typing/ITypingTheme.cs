using OctoType.Application.Models.Typing.Themes;
using OctoType.Domain.Typing;

namespace OctoType.Application.Interfaces.Typing;

public interface ITypingTheme
{
    TypingStyle GetStyle(TypingCharEnumState state);
}
