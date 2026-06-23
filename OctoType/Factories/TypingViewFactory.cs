using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.MVVM.Views;
using OctoType.ViewModels.Typing;

namespace OctoType.Factories;

public class TypingViewFactory : ITypingViewFactory
{
    private readonly ITypingThemeProvider _typingThemeProvider;
    private readonly IStringsProvider _stringsProviderService;
    private readonly IInputCharMapperService _charMapper;

    public TypingViewFactory(
        ITypingThemeProvider typingThemeProvider,
        IStringsProvider stringsProviderService,
        IInputCharMapperService charMapper)
    {
        _typingThemeProvider = typingThemeProvider;
        _stringsProviderService = stringsProviderService;
        _charMapper = charMapper;
    }

    public ContentPage CreateTypingView()
    {
        TypingViewModel typingviewmodel = new(_stringsProviderService, _charMapper, _typingThemeProvider);

        TypingView typingView = new (typingviewmodel);

        return typingView;
    }
}
