using OctoType.Interfaces;
using OctoType.Models.UI.Typing;

namespace OctoType.Factories;

public class TypingLineStateFactory : ITypingLineStateFactory
{
    private readonly ITypingCharFactory _factory;
    public TypingLineStateFactory(ITypingCharFactory factory)
    {
        _factory = factory;
    }

    public TypingLineState Create(string line)
    {
        return new TypingLineState(_factory, line);
    }
}
