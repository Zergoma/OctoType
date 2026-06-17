using OctoType.Application;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Themes;

namespace OctoType.Infrastructure.Themes;

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
            Result<TypingThemeDefinition> resuUser =
                await _userThemesLoader.LoadAsync(name);

            if (resuUser.Success)
            {
                JsonTypingTheme userTheme = new(resuUser.Value!);
                _themes.Add(name, userTheme);
                return userTheme;
            }
        }

        {
            Result<TypingThemeDefinition> resuAsset =
                    await _assetThemesLoader.LoadAsync(name);

            if (!resuAsset.Success)
            {
                return null;
            }

            JsonTypingTheme assetTheme = new(resuAsset.Value!);
            _themes.Add(name, assetTheme);
            return assetTheme;
        }
    }
}