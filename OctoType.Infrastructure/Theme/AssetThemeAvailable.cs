using System.Text.Json;

using OctoType.Application.Interfaces;

namespace OctoType.Infrastructure.Theme;

public class AssetThemeAvailable
{
    private readonly IAssetReader _assetReader;
    public AssetThemeAvailable(IAssetReader assetReader)
    {
        _assetReader = assetReader;
    }
    public async Task<IEnumerable<string>> GetAvailableThemesAsync()
    {
        await using Stream stream =
            await _assetReader.OpenAsync("themes.index.json");

        var themes =
            await JsonSerializer.DeserializeAsync<List<string>>(stream);

        return themes?.AsEnumerable() ?? [];
    }
}
