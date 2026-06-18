using System.Text.Json;

using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Infrastructure.Theme.Models;
using OctoType.Infrastructure.Theme.Interfaces;

namespace OctoType.Infrastructure.Theme;

public class AssetThemesLoader : IThemeLoader
{
    private readonly IAssetReader _assetReader;

    public AssetThemesLoader(IAssetReader assetReader)
    {
        _assetReader = assetReader;
    }

    public async Task<Result<ThemeFileModel>> LoadAsync(string themeName)
    {
        string path = $"{themeName}.json";

        try
        {
            ThemeFileModel? theme = null;

            await using Stream stream =
                await _assetReader.OpenAsync(path);

            theme =
                await JsonSerializer.DeserializeAsync<ThemeFileModel>(
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
               .Fail($"{ex.Message}");
        }
    }
}
