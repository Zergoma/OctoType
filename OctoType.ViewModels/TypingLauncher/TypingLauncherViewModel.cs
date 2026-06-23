using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Engine;
using OctoType.Application.Models.Typing.Exercices;
using OctoType.Application.Orchestrators;

namespace OctoType.ViewModels.TypingLauncher;

public partial class TypingLauncherViewModel : ObservableObject
{
    private readonly ITypingExercicesStorage _typingExerciceStorage;
    private readonly INavigationService _navigation;
    private ITypingExercicesEngine? _typingExerciceEngine;
    private ICreateStringProviderOrchestrator _createStringProviderOrchestrator;

    public ObservableCollection<ExerciceItemViewModel> AllExercice { get; set; } = [];
    private bool _isInit = false;
    private readonly List<KeyBoardLayoutDto> _keyboardLayoutAvailableElem;

    public TypingLauncherViewModel(
        ITypingExercicesStorage typingExerciceStorage,
        INavigationService navigation,
        ICreateStringProviderOrchestrator createStringProviderOrchestrator,
        IKeyBoardLayoutAvailableService keyboardLayoutAvailableService)
    {
        _typingExerciceStorage = typingExerciceStorage;
        _navigation = navigation;
        _createStringProviderOrchestrator = createStringProviderOrchestrator;
        _keyboardLayoutAvailableElem = keyboardLayoutAvailableService.GetKeyBoardAvailable();
    }

    public IReadOnlyList<KeyBoardLayoutDto> KeyboardLayoutAvailable => _keyboardLayoutAvailableElem;
    public KeyBoardLayoutDto? KeyboardLayoutSelected { get; set; }

    public async Task Initilization()
    {
        if (_isInit)
        {
            return;
        }
        _isInit = true;

        Result<TypingExercices> exercicesListLoadedResult =
            await _typingExerciceStorage.LoadAsync();

        if (exercicesListLoadedResult.Success)
        {
            AllExercice.Clear();

            List<TypingExercise> exercises = exercicesListLoadedResult.GetValue.Exercices;

            _typingExerciceEngine = new TypingExercicesEngine(exercicesListLoadedResult.GetValue, 0);

            for (int i = 0; i < exercises.Count; i++)
            {
                AllExercice.Add(new ExerciceItemViewModel(exercises[i], i));
            }
        }
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



    public string ExerciceName => ExerciceSelected?.Name ?? "Exercice Name";
    public string ExerciceDescription => ExerciceSelected?.Desciption ?? "Exercice Description";
    public string ExerciceLetters => ExerciceSelected?.Letters ?? "Exercice Letters";



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
            if(!stringProviderResult.Success)
            {
                return;
            }

            // TODO
            // Think about ExerciceEngine inside
            // This give the ability to autolaunch next exercice
            // Was the original thinking
            // But firts... MAke it works, and that's now working perfectly ^^
            // So next time
            await _navigation.NavigateToTypingExerciseAsync(stringProviderResult.GetValue);
        }
    }
}
