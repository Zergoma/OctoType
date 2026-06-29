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

    public TypingViewFactory(
        ITypingThemeProvider typingThemeProvider,
        IInputCharMapperService charMapper)
    {
        _typingThemeProvider = typingThemeProvider;
        _charMapper = charMapper;
    }

    public async Task<Result<ContentPage>> CreateTypingViewAsync(
        IStringsProvider stringProvider,
        INavigationService navigationService)
    {
        TypingViewModel typingviewmodel = new(_charMapper, _typingThemeProvider);
        await typingviewmodel.LoadTextAsync(stringProvider);

        TypingView typingView = new(typingviewmodel, navigationService);

        return Result<ContentPage>
            .Ok(typingView);
    }
}
