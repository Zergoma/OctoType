using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.Logging;

using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Themes;
using OctoType.Application.Models.Typing.Engine;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.ViewModels.TypingLauncher;



public partial class TypingLauncherViewModel : ObservableObject
{
    public event Func<int, Task>? KeyboardLayoutChanged;

    private readonly ITypingExercicesStorage _typingExerciceStorage;
    private readonly INavigationService _navigation;
    private readonly ICreateStringProviderOrchestrator _createStringProviderOrchestrator;
    private readonly IThemeChangerService _themeChangerService;
    private readonly IThemeIconeCodeProvider _themeIconeProvider;
    private ITypingExercicesEngine? _typingExerciceEngine;
    private ILogger<TypingLauncherViewModel> _logger;

    public ObservableCollection<ExerciceItemViewModel> AllExercice { get; set; } = [];
    private readonly List<KeyBoardLayoutDto> _keyboardLayoutAvailableElem;

    ThemeStateConfiguration _themeSwitch = ThemeStateConfiguration.Dark;

    public TypingLauncherViewModel(
        ITypingExercicesStorage typingExerciceStorage,
        INavigationService navigation,
        ICreateStringProviderOrchestrator createStringProviderOrchestrator,
        IKeyBoardLayoutAvailableService keyboardLayoutAvailableService,
        IThemeChangerService themeChangerService,
        IThemeIconeCodeProvider themeIconeProvider,
        ILogger<TypingLauncherViewModel> logger)
    {
        _typingExerciceStorage = typingExerciceStorage;
        _navigation = navigation;
        _createStringProviderOrchestrator = createStringProviderOrchestrator;
        _keyboardLayoutAvailableElem = keyboardLayoutAvailableService.GetKeyBoardAvailable();

        AllExercice.CollectionChanged += (_, _) =>
        {
            OnPropertyChanged(nameof(HasExercice));
            OnPropertyChanged(nameof(HasNoExercice));
        };

        _themeChangerService = themeChangerService;
        _themeSwitch = _themeChangerService.ApplyUserSelectedTheme();

        _themeIconeProvider = themeIconeProvider;
        IconeTheme = _themeIconeProvider.GetIconeCode(_themeSwitch);
        _logger = logger;
    }

    private void SetKeyboardLayout(int id)
    {
        KeyBoardLayoutDto? itemKeyboard = _keyboardLayoutAvailableElem.Find(k => (int)k.KeyBoardCode == id);
        KeyboardLayoutSelected = itemKeyboard;
    }

    public IReadOnlyList<KeyBoardLayoutDto> KeyboardLayoutAvailable => _keyboardLayoutAvailableElem;

    [ObservableProperty]
    public partial KeyBoardLayoutDto? KeyboardLayoutSelected { get; set; }
    partial void OnKeyboardLayoutSelectedChanged(KeyBoardLayoutDto? value)
    {
        if (value == null)
            return;

        KeyboardLayoutChanged?.Invoke((int)value.KeyBoardCode);
    }

    public bool HasExercice => AllExercice.Count > 0;

    public bool HasNoExercice => !HasExercice;

    public async Task<Result<bool>> InitilizationAsync(int keyboardLayoutDtoId)
    {
        SetKeyboardLayout(keyboardLayoutDtoId);
        if (KeyboardLayoutSelected == null)
        {
            return Result<bool>
                .Fail($"keyboard id {keyboardLayoutDtoId} doesn't exist");
        }

        Result<TypingExercices> exercicesListLoadedResult =
            await _typingExerciceStorage.LoadAsync(KeyboardLayoutSelected.KeyBoardCode);

        if (exercicesListLoadedResult.Success)
        {
            AllExercice.Clear();

            List<TypingExercise> exercises = exercicesListLoadedResult.GetValue.Exercices;

            _typingExerciceEngine = new TypingExercicesEngine(exercicesListLoadedResult.GetValue, 0);

            for (int i = 0; i < exercises.Count; i++)
            {
                AllExercice.Add(new ExerciceItemViewModel(exercises[i], i));
            }
            Result<bool>
                .Ok(true);
        }
        
        return Result<bool>
            .Fail(exercicesListLoadedResult.Error);
    }


    public bool IsSelectedExercice => ExerciceSelected != null;
    public bool IsNoSelection => !IsSelectedExercice;

    public int IdxSelected => ExerciceSelected?.Idx ?? -1;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSelectedExercice))]
    [NotifyPropertyChangedFor(nameof(IsNoSelection))]
    [NotifyPropertyChangedFor(nameof(ExerciceName))]
    [NotifyPropertyChangedFor(nameof(ExerciceDescription))]
    [NotifyPropertyChangedFor(nameof(ExerciceLetters))]
    public partial ExerciceItemViewModel? ExerciceSelected { get; set; }



    public string ExerciceName => ExerciceSelected?.Name ?? "Exercices Name";
    public string ExerciceDescription => ExerciceSelected?.Desciption ?? "Exercices Description";
    public string ExerciceLetters => ExerciceSelected?.Letters ?? "Exercices Letters";



    [RelayCommand]
    public void Select(ExerciceItemViewModel exerciceSelected)
    {
        if (ExerciceSelected == exerciceSelected)
            return;

        ExerciceSelected?.IsSelected = false;
        ExerciceSelected = exerciceSelected;
        ExerciceSelected.IsSelected = true;

        _typingExerciceEngine?.SetIdx(ExerciceSelected.Idx);
    }

    [RelayCommand]
    public async Task Launch()
    {
        if (_typingExerciceEngine == null)
            return;

        Result<TypingExercise> currentExerciceResult = _typingExerciceEngine.CurrentExercice();
        if (!currentExerciceResult.Success)
        {
            //return Result<ContentPage>.Fail(currentExerciceResult.Error);
            return;
        }

        if (KeyboardLayoutSelected is KeyBoardLayoutDto keyboardLayoutDto)
        {
            TypingExercise exer = currentExerciceResult.GetValue;
            Result<IStringsProvider> stringProviderResult = _createStringProviderOrchestrator.Create(exer, keyboardLayoutDto);
            if (!stringProviderResult.Success)
            {
                return;
            }

            _logger.LogInformation(
            "Exercise started {ExerciseId} {ExerciceName}",
            exer.Id,
            exer.Name);

            // TODO
            // Think about ExerciceEngine inside
            // This give the ability to autolaunch next exercice
            // Was the original thinking
            // But firts... MAke it works, and that's now working perfectly ^^
            // So next time
            await _navigation.NavigateToTypingExerciseAsync(stringProviderResult.GetValue);
        }
    }

    [RelayCommand]
    public async Task GoToExerciceGenerator()
    {
        await _navigation.NavigateToExerciceGeneratorAsync();
    }

    [RelayCommand]
    public async Task GoToUpdateExercice()
    {
        if (ExerciceSelected == null)
            return;

        await _navigation.NavigateToUpdateExerciceAsync(ExerciceSelected.Guid);
    }


    [ObservableProperty]
    public partial string IconeTheme { get; set; }

    [RelayCommand]
    public void ChangeTheme()
    {
        _themeSwitch = _themeSwitch switch
        {
            ThemeStateConfiguration.Dark => ThemeStateConfiguration.Light,
            ThemeStateConfiguration.Light => ThemeStateConfiguration.System,
            ThemeStateConfiguration.System => ThemeStateConfiguration.Dark,
            _ => throw new NotImplementedException(),
        };


        switch(_themeSwitch)
        {
            case ThemeStateConfiguration.Dark: _themeChangerService.SetDark();break;
            case ThemeStateConfiguration.Light: _themeChangerService.SetLight();break;
            case ThemeStateConfiguration.System: _themeChangerService.SetToSystem();break;
            default: throw new NotImplementedException();
        };

        IconeTheme = _themeIconeProvider.GetIconeCode(_themeSwitch);
    }
}
