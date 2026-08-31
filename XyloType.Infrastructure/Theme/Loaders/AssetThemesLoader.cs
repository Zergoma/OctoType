using System.Text.Json;

using XyloType.Application;
using XyloType.Application.Interfaces;
using XyloType.Infrastructure.Theme.Models;

namespace XyloType.Infrastructure.Theme.Loaders;

public class AssetThemesLoader : IThemeLoader
{
    private readonly IAssetReader _assetReader;

    public AssetThemesLoader(IAssetReader assetReader)
    {
        _assetReader = assetReader;
    }

    public async Task<Result<ThemeFileModel>> LoadAsync(string themeName, CancellationToken cancellationToken = default)
    {
        string path = $"{themeName}.json";

        try
        {

            await using Stream stream =
                await _assetReader.OpenAsync(path);

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
               .Fail($"{ex.Message}");
        }
    }
}
