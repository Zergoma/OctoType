using System.Text.Json;

using OctoType.Application;
using OctoType.Application.Mappers;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Models.Typing.Themes;

namespace OctoType.Infrastructure.Theme;

public class UserThemesLoader : IThemeLoader
{
    private readonly IThemePathProvider _AppPathProvider;

    public UserThemesLoader(IThemePathProvider appPathProvider)
    {
        _AppPathProvider = appPathProvider;
    }

    public async Task<Result<ThemeDto>> LoadAsync(string themeName)
    {
        string path =
            Path.Combine(
                _AppPathProvider.ThemesDirectory,
                $"{themeName}.json");

        if (!File.Exists(path))
        {
            return Result<ThemeDto>
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
                return Result<ThemeDto>
                    .Fail($"Unable to load theme '{themeName}'.");
            }

            return Result<ThemeDto>
                .Ok(theme.ToDto());

        }
        catch (Exception ex)
        {
            return Result<ThemeDto>
                .Fail($"{ex}");
        }

    }
}