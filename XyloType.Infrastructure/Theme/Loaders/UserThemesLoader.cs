using System.Text.Json;

using XyloType.Application;
using XyloType.Application.Interfaces;
using XyloType.Infrastructure.Theme.Models;

namespace XyloType.Infrastructure.Theme.Loaders;

public class UserThemesLoader : IThemeLoader
{
    private readonly IThemePathProvider _AppPathProvider;

    public UserThemesLoader(IThemePathProvider appPathProvider)
    {
        _AppPathProvider = appPathProvider;
    }

    public async Task<Result<ThemeFileModel>> LoadAsync(string themeName, CancellationToken cancellationToken = default)
    {
        string path =
            Path.Combine(
                _AppPathProvider.ThemesDirectory,
                $"{themeName}.json");

        if (!File.Exists(path))
        {
            return Result<ThemeFileModel>
                .Fail($"File not found : {path}");
        }

        try
        {
            using Stream stream = File.OpenRead(path);

            ThemeFileModel? theme = 
                await JsonSerializer.DeserializeAsync<ThemeFileModel>(
                stream,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                },
                cancellationToken);

            if (theme == null)
            {
                return Result<ThemeFileModel>
                    .Fail($"Unable to load theme '{themeName}'.");
            }

            return Result<ThemeFileModel>
                .Ok(theme);

        }
        catch (Exception ex)
        {
            return Result<ThemeFileModel>
                .Fail($"{ex}");
        }

    }
}