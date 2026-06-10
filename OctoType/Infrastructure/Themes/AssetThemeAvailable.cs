using System.Text.Json;

namespace OctoType.Infrastructure.Themes;

public class AssetThemeAvailable
{
    public async Task<IEnumerable<string>> GetAvailableThemesAsync()
    {
        await using Stream stream =
            await FileSystem.OpenAppPackageFileAsync(
                "themes.index.json");

        var themes =
            await JsonSerializer.DeserializeAsync<List<string>>(stream);

        return themes?.AsEnumerable() ?? [];
    }
}
