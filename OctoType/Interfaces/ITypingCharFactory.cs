using OctoType.Domain.Typing;
using OctoType.Models.UI.Typing;
namespace OctoType.Interfaces;

public interface ITypingCharFactory
{
    Task<TypingCharState> CreateAsync(char c, TypingCharEnumState state, string themeName);
}
