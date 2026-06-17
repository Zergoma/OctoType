using OctoType.Models.UI.Typing;

namespace OctoType.Interfaces
{
    public interface ITypingLineStateFactory
    {
        TypingLineState CreateLineState(string line, ITypingTheme theme);
    }
}