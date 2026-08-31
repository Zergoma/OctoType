using XyloType.Application;
using XyloType.Infrastructure.Theme.Models;

namespace XyloType.Infrastructure.Theme.Loaders;

public interface IThemeLoader
{
    Task<Result<ThemeFileModel>> LoadAsync(string themeName, CancellationToken cancellationToken = default);
}
