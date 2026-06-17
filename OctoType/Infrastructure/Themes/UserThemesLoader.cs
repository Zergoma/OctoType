using System.Text.Json;

using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Application.Models.Typing.Themes;

namespace OctoType.Infrastructure.Themes;

public class UserThemesLoader : IThemeLoader
{
    private readonly IThemePathProvider _AppPathProvider;

    public UserThemesLoader(IThemePathProvider appPathProvider)
    {
        _AppPathProvider = appPathProvider;
    }

    public async Task<Result<TypingThemeDefinition>> LoadAsync(string themeName)
    {
        string path = 
            Path.Combine(
                _AppPathProvider.ThemesDirectory, 
                $"{themeName}.json");

        if (!File.Exists(path))
        {
            return Result<TypingThemeDefinition>
                .Fail($"File not found : {path}");
        }

        TypingThemeDefinition? theme = null;
        try
        {
            using Stream stream = File.OpenRead(path);

            theme = await JsonSerializer.DeserializeAsync<TypingThemeDefinition>(
                stream,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


        }
        catch (Exception ex)
        {
            return Result<TypingThemeDefinition>
                .Fail($"{ex}");
        }

        if (theme == null)
        {
            return Result<TypingThemeDefinition>
                .Fail($"Unable to load theme '{themeName}'.");
        }

        return Result<TypingThemeDefinition>
            .Ok(theme);
    }
}