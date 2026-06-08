using System.Text.Json;

using OctoType.Application;
using OctoType.Interfaces;
using OctoType.Models.UI.Typing;

namespace OctoType.Infrastructure.Themes;

public class AssetThemesLoader : IThemeLoader
{
    public async Task<Result<TypingThemeDefinition>> LoadAsync(string themeName)
    {
        string path =
            $"{themeName}.json";

        TypingThemeDefinition? theme = null;
        try
        {

            await using Stream stream =
                await FileSystem.OpenAppPackageFileAsync(path);

            theme =
                await JsonSerializer.DeserializeAsync<TypingThemeDefinition>(
                    stream,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }
        catch (Exception ex)
        {
            return Result<TypingThemeDefinition>
               .Fail($"{ex.Message}");
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
