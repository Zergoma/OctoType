using System.Text.Json;

using OctoType.Application;
using OctoType.Application.Mappers;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Infrastructure.Theme.Models;
using OctoType.Infrastructure.Theme.Mappers;

namespace OctoType.Infrastructure.Theme;

public class AssetThemesLoader : IThemeLoader
{
    private readonly IAssetReader _assetReader;

    public AssetThemesLoader(IAssetReader assetReader)
    {
        _assetReader = assetReader;
    }

    public async Task<Result<ThemeDto>> LoadAsync(string themeName)
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
                return Result<ThemeDto>
                    .Fail($"Unable to load theme '{themeName}'.");
            }

            return Result<ThemeDto>
                .Ok(theme.ToDto());
        }
        catch (Exception ex)
        {
            return Result<ThemeDto>
               .Fail($"{ex.Message}");
        }
    }
}
