using OctoType.Application;
using OctoType.Models.UI.Typing;

namespace OctoType.Interfaces;

public interface IThemeLoader
{
    Task<Result<TypingThemeDefinition>> LoadAsync(string themeName);
}
