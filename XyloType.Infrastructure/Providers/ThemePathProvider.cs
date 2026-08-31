using XyloType.Application.Interfaces;
using XyloType.Domain.Constaintes;

namespace XyloType.Infrastructure.Providers;

public class ThemePathProvider : IThemePathProvider
{
    public ThemePathProvider()
    {
        // auto create the user specific directory
        Directory.CreateDirectory(ThemesDirectory);
    }

    public string ThemesDirectory
        => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                AppNameData.AppName,
                "themes");
}
