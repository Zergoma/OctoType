using OctoType.Application;
using OctoType.Infrastructure.Theme.Models;

namespace OctoType.Infrastructure.Theme.Interfaces;

public interface IThemeLoader
{
    Task<Result<ThemeFileModel>> LoadAsync(string themeName);
}
