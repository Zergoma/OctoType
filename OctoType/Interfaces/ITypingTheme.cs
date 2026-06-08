using OctoType.Domain.Enums;
using OctoType.Models.UI.Typing;

namespace OctoType.Interfaces;

public interface ITypingTheme
{
    TypingStyle GetStyle(TypingCharEnumState state);
}
