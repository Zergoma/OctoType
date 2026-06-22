using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DomainTyping = OctoType.Domain.Typing;
using AppInterfaces = OctoType.Application.Interfaces;
using AppInterfacesTyping = OctoType.Application.Interfaces.Typing;

using OctoType.Application;
using OctoType.Domain.Typing;
using System.Diagnostics;

namespace OctoType.ViewModels.Typing;

public partial class TypingViewModel : ObservableObject
{
    public event Action<int>? LineChanged;
    public DomainTyping.TypingSession Session { get; } = new();
    public ObservableCollection<TypingLineStateViewModel> LinesStates { get; } = [];

    private readonly AppInterfacesTyping.ITypingThemeProvider _typingThemeProvider;


    private readonly AppInterfaces.IStringsProvider _stringsProviderService;
    private readonly AppInterfaces.IInputCharMapperService _charMapper;

    public TypingViewModel(
        AppInterfaces.IStringsProvider stringsProviderService,
        AppInterfaces.IInputCharMapperService charMapper,
        AppInterfacesTyping.ITypingThemeProvider typingThemeProvider)
    {
        _stringsProviderService = stringsProviderService;
        _charMapper = charMapper;

        Session.LineChanged += (int lineNumber) =>
        {
            LineChanged?.Invoke(lineNumber);
        };
        _typingThemeProvider = typingThemeProvider;
    }


    public bool StopOnErrorEnable
    {
        get => Session.StopOnError;
        set
        {
            if (Session.StopOnError == value)
                return;
            Session.StopOnError = value;
            OnPropertyChanged(nameof(StopOnErrorEnable));
            OnPropertyChanged(nameof(StopOnErrorTxt));
        }
    }

    [RelayCommand]
    public async Task SwitchStopOnError()
        => StopOnErrorEnable = !StopOnErrorEnable;

    public string StopOnErrorTxt
        => StopOnErrorEnable ? "Arret sur erreur" : "Continue sur erreur";

    public bool BackReturnEnable
    {
        get => Session.BackReturnEnable;
        set
        {
            if (Session.BackReturnEnable == value)
                return;

            Session.BackReturnEnable = value;
            OnPropertyChanged(nameof(BackReturnEnable));
            OnPropertyChanged(nameof(BackReturnTxt));
        }
    }

    [RelayCommand]
    public async Task SwitchBackReturn()
        => BackReturnEnable = !BackReturnEnable;

    public string BackReturnTxt
        => BackReturnEnable ? "Retour arrière activé" : "Retour arrière interdit";

    // TODO
    // This fct is for dev purpose
    // A sort of will be required to get exercices
    public async Task LoadTextAsync()
    {
        Session.Lines.Clear();
        LinesStates.Clear();

        // TODO
        // to property, + user access
        Result<AppInterfacesTyping.ITypingTheme> themeResu =
            await _typingThemeProvider.GetThemeAsync("OctoType_Typing_Theme");
        
        if(!themeResu.Success)
        {
            return;
        }

        Result<IEnumerable<string>> getStringResult =
            await _stringsProviderService.GetStringsAsync();

        if(!getStringResult.Success)
        {
            Debug.WriteLine(getStringResult.Error);
            return;
        }

        string[] dataLines = [.. getStringResult.GetValue];

        ArgumentOutOfRangeException.ThrowIfLessThan(dataLines.Length, 1);

        foreach (string line in dataLines)
        {
            DomainTyping.TypingLine typingLine = new (line);
            Session.Lines.Add(typingLine);

            TypingLineStateViewModel typingLineState =  new (themeResu.GetValue, typingLine);
            LinesStates.Add(typingLineState);
        }

        Session.ResetProgression();
    }

    public DomainTyping.TypingStatus ProcessInput(char input)
    {
        TypingStatus processStatus = Session.ProcessInput(input, _charMapper.Map);

        return processStatus;
    }
}
