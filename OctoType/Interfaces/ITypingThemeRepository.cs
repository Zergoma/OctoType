namespace OctoType.Interfaces;

public interface ITypingThemeRepository
{
    bool IsThemeLoaded(string name);
    Task<ITypingTheme?> GetTheme(string name); 
}
