namespace OctoType.Application.Interfaces.Typing;

public interface ITypingThemeRepository
{
    bool IsThemeLoaded(string name);
    Task<ITypingTheme?> GetTheme(string name); 
}
