using System.Text.Json;

using XyloType.Application.Interfaces;

namespace XyloType.Infrastructure.Theme.Availables;

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
