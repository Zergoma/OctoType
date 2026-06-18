namespace OctoType.Application.Interfaces.Typing;

public interface ITypingThemeProvider
{
    bool ContainsTheme(string name);
    Task<Result<ITypingTheme>> GetThemeAsync(string name, CancellationToken cancellationToken = default); 
}
