using System.Text.Json;

using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Infrastructure.Theme.Models;
using OctoType.Infrastructure.Theme.Interfaces;

namespace OctoType.Infrastructure.Theme;

public class UserThemesLoader : IThemeLoader
{
    private readonly IThemePathProvider _AppPathProvider;

    public UserThemesLoader(IThemePathProvider appPathProvider)
    {
        _AppPathProvider = appPathProvider;
    }

    public async Task<Result<ThemeFileModel>> LoadAsync(string themeName)
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
            ThemeFileModel? theme = null;
            using Stream stream = File.OpenRead(path);

            theme = await JsonSerializer.DeserializeAsync<ThemeFileModel>(
                stream,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

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