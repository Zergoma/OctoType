using XyloType.Application;
using XyloType.Application.Models.Themes;

namespace XyloType.Application.Interfaces.Typing;

public interface ITypingThemeProvider
{
    bool ContainsTheme(string name);
    Task<Result<ITypingTheme>> GetThemeAsync(string name, ThemeState themeState, CancellationToken cancellationToken = default); 
}
