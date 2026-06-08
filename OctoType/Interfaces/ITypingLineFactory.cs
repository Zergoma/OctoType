using OctoType.Models.UI.Typing;

namespace OctoType.Interfaces
{
    public interface ITypingLineStateFactory
    {
        TypingLineState Create(string line);
    }
}