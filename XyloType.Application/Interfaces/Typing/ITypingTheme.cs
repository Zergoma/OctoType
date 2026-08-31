using XyloType.Application.Models.Typing.Themes;
using XyloType.Domain.Typing;

namespace XyloType.Application.Interfaces.Typing;

public interface ITypingTheme
{
    TypingStyle GetStyle(TypingCharState charState);
}
