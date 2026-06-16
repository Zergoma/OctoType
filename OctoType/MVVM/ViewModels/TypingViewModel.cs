using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using AppInterfaces = OctoType.Application.Interfaces;
using OctoType.Interfaces;
using OctoType.Models.UI.Typing;
using OctoType.Models;
using OctoType.Domain.Enums;
using CommunityToolkit.Mvvm.Input;

namespace OctoType.MVVM.ViewModels;

public partial class TypingViewModel : ObservableObject
{
    public event Action<int>? LineChanged;
    public TypingSession Session { get; } = new();

    public ObservableCollection<TypingLineState> Lines => Session.Lines;

    private readonly AppInterfaces.IStringsProviderService _stringsProviderService;
    private readonly AppInterfaces.IInputCharMapperService _charMapper;
    private readonly ITypingLineStateFactory _typingLineFactory;

    public TypingViewModel(
        AppInterfaces.IStringsProviderService stringsProviderService,
        AppInterfaces.IInputCharMapperService charMapper,
        ITypingLineStateFactory typingLineFactory)
    {
        _stringsProviderService = stringsProviderService;
        _charMapper = charMapper;
        _typingLineFactory = typingLineFactory;

        Session.LineChanged += (int lineNumber) =>
        {
            LineChanged?.Invoke(lineNumber);
        };
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

    

    public async Task LoadTextAsync()
    {
        Lines.Clear();

        string[] dataLines =
            [.. (await _stringsProviderService.GetStringsAsync())];

        ArgumentOutOfRangeException.ThrowIfLessThan(dataLines.Length, 1);

        foreach (string line in dataLines)
        {
            TypingLineState lineVm = _typingLineFactory.Create(line);
            Lines.Add(lineVm);
        }

        Session.Reset();
    }

    public TypingStatus ProcessInput(char input)
    {
        return Session.ProcessInput(input, _charMapper.Map);
    }
}
