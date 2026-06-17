using OctoType.Application.Interfaces;

namespace OctoType.Infrastructure.Theme;

public class UserThemeAvailable
{
    private readonly IThemePathProvider _AppPathProvider;
    public UserThemeAvailable(
        IThemePathProvider appPathProvider)
    {
        _AppPathProvider = appPathProvider;
    }
    public Task<IEnumerable<string>> GetAvailableThemesAsync()
    {
        string themeFolderPath = _AppPathProvider.ThemesDirectory;

        if (!Directory.Exists(themeFolderPath))
        {
            Directory.CreateDirectory(themeFolderPath);
        }

        IEnumerable<string> files =
            Directory.EnumerateFiles(
                themeFolderPath,
                "*_Typing_Theme.json",
                SearchOption.TopDirectoryOnly);

        IEnumerable<string> themeNames =
            files
            .Select(Path.GetFileNameWithoutExtension)
            .OfType<string>();

        return Task.FromResult(themeNames);
    }
}