using OctoType.Application;
using OctoType.Application.Interfaces.Typing;
using OctoType.Infrastructure.Theme.Loaders;
using OctoType.Infrastructure.Theme.Mappers;
using OctoType.Infrastructure.Theme.Models;

namespace OctoType.Infrastructure.Theme.Providers;

public class TypingThemeProvider : ITypingThemeProvider
{
    private readonly AssetThemesLoader _assetThemesLoader;
    private readonly UserThemesLoader _userThemesLoader;
    private readonly Dictionary<string, ITypingTheme> _themes = [];
    public TypingThemeProvider(
        AssetThemesLoader themeLoader,
        UserThemesLoader userThemesLoader)
    {
        _assetThemesLoader = themeLoader;
        _userThemesLoader = userThemesLoader;
    }
    public bool ContainsTheme(string name)
    {
        return _themes.ContainsKey(name);
    }

    public async Task<Result<ITypingTheme>> GetThemeAsync(string name, CancellationToken cancellationToken = default)
    {
        if (_themes.TryGetValue(name, out ITypingTheme? theme))
        {
            return Result<ITypingTheme>
                .Ok(theme);
        }


        Result<ThemeFileModel> userResult =
            await _userThemesLoader.LoadAsync(name);

        if (userResult.Success)
        {
            ITypingTheme userTheme = userResult.GetValue.ToTheme();
            _themes[name] = userTheme;
            return Result<ITypingTheme>
                .Ok(userTheme);
        }



        Result<ThemeFileModel> assetResult =
                await _assetThemesLoader.LoadAsync(name);

        if (!assetResult.Success)
        {
            return Result<ITypingTheme>
                .Fail($"Theme: {name} doesn't exist");
        }

        ITypingTheme assetTheme = assetResult.GetValue.ToTheme();
        _themes[name] = assetTheme;
        return Result<ITypingTheme>
            .Ok(assetTheme);

    }
}