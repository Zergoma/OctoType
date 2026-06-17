using OctoType.Application.Models.Typing.Themes;

namespace OctoType.Application.Interfaces;

public interface IThemeLoader
{
    Task<Result<TypingThemeDefinition>> LoadAsync(string themeName);
}
