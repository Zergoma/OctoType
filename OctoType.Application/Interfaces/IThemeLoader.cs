using OctoType.Application.DTOs;

namespace OctoType.Application.Interfaces;

public interface IThemeLoader
{
    Task<Result<ThemeDto>> LoadAsync(string themeName);
}
