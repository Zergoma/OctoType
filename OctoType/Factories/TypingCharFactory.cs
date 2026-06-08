using OctoType.Domain.Enums;
using OctoType.Interfaces;
using OctoType.Models.UI.Typing;

namespace OctoType.Factories;

public class TypingCharFactory : ITypingCharFactory
{
    private readonly ITypingThemeRepository _typingThemeRepo;

    public TypingCharFactory(
        ITypingThemeRepository typingThemeRepo)
    {
        _typingThemeRepo = typingThemeRepo;
    }

    public async Task<TypingCharState> CreateAsync(char c, TypingCharEnumState state, string themeName)
    {
        ITypingTheme? theme = await _typingThemeRepo.GetTheme(themeName);
        
        ArgumentNullException.ThrowIfNull(theme, nameof(theme));
        
        return new TypingCharState(theme)
        {
            Character = c,
            State = state,
        };
    }
}
