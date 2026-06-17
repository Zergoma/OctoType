using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces.Typing;

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
            Result<ThemeDto> resuUser =
                await _userThemesLoader.LoadAsync(name);

            if (resuUser.Success)
            {
                ITypingTheme userTheme = TypingThemeMapper.ToTheme(resuUser.GetValue);
                _themes[name] = userTheme;
                return userTheme;
            }
        }

        {
            Result<ThemeDto> resuAsset =
                    await _assetThemesLoader.LoadAsync(name);

            if (!resuAsset.Success)
            {
                return null;
            }

            ITypingTheme assetTheme = TypingThemeMapper.ToTheme(resuAsset.GetValue);
            _themes[name] = assetTheme;
            return assetTheme;
        }
    }
}