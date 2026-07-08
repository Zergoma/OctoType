using OctoType.Application.Models.Themes;

namespace OctoType.Application.Interfaces.Typing;

public interface ITypingThemeProvider
{
    bool ContainsTheme(string name);
    Task<Result<ITypingTheme>> GetThemeAsync(string name, ThemeState themeState, CancellationToken cancellationToken = default); 
}
