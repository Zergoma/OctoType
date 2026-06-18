using OctoType.Application;
using OctoType.Application.Interfaces.Typing;
using OctoType.Infrastructure.Theme.Mappers;
using OctoType.Infrastructure.Theme.Models;

namespace OctoType.Infrastructure.Theme;

public class TypingThemeRepository : ITypingThemeRepository
{
    private readonly AssetThemesLoader _assetThemesLoader;
    private readonly UserThemesLoader _userThemesLoader;
    private readonly Dictionary<string, ITypingTheme> _themes = [];
    public TypingThemeRepository(
        AssetThemesLoader themeLoader,
        UserThemesLoader userThemesLoader)
    {
        _assetThemesLoader = themeLoader;
        _userThemesLoader = userThemesLoader;
    }
    public bool IsThemeLoaded(string name)
    {
        return _themes.ContainsKey(name);
    }

    public async Task<ITypingTheme?> GetTheme(string name)
    {
        if (_themes.TryGetValue(name, out ITypingTheme? theme))
        {
            return theme;
        }

        {
            Result<ThemeFileModel> resuUser =
                await _userThemesLoader.LoadAsync(name);

            if (resuUser.Success)
            {
                ITypingTheme userTheme = resuUser.GetValue.ToTheme();
                _themes[name] = userTheme;
                return userTheme;
            }
        }

        {
            Result<ThemeFileModel> resuAsset =
                    await _assetThemesLoader.LoadAsync(name);

            if (!resuAsset.Success)
            {
                return null;
            }

            ITypingTheme assetTheme = resuAsset.GetValue.ToTheme();
            _themes[name] = assetTheme;
            return assetTheme;
        }
    }
}