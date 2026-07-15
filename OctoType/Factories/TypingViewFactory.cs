using Microsoft.Extensions.Logging;

using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.MVVM.Views;
using OctoType.ViewModels.Typing;

namespace OctoType.Factories;

public class TypingViewFactory : ITypingViewFactory
{
    private readonly ITypingThemeProvider _typingThemeProvider;
    private readonly IInputCharMapperService _charMapper;
    private readonly IThemeChangerService _themeChangerService;


    public TypingViewFactory(
        ITypingThemeProvider typingThemeProvider,
        IInputCharMapperService charMapper,
        IThemeChangerService themeChangerService,
        ILogger<TypingView> logger)
    {
        _typingThemeProvider = typingThemeProvider;
        _charMapper = charMapper;
        _themeChangerService = themeChangerService;
    }

    public async Task<Result<ContentPage>> CreateTypingViewAsync(
        IStringsProvider stringProvider,
        INavigationService navigationService)
    {
        TypingViewModel typingviewmodel =
            new(
                _charMapper,
                _typingThemeProvider,
                _themeChangerService);

        await typingviewmodel.LoadTextAsync(stringProvider);

        TypingView typingView =
            new(
                typingviewmodel,
                navigationService);

        return Result<ContentPage>
            .Ok(typingView);
    }
}
