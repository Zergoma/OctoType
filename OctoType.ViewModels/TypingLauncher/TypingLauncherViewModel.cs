using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Engine;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.ViewModels.TypingLauncher;

public partial class TypingLauncherViewModel : ObservableObject
{
    private readonly ITypingExercicesStorage _typingExerciceStorage;
    private readonly INavigationService _navigation;
    private TypingExercicesEngine? _typingExerciceEngine;
    public ObservableCollection<ExerciceItemViewModel> AllExercice { get; set; } = [];
    private bool _isInit = false;

    public TypingLauncherViewModel(
        ITypingExercicesStorage typingExerciceStorage,
        INavigationService navigation)
    {
        _typingExerciceStorage = typingExerciceStorage;
        _navigation = navigation;
    }

    public async Task Initilization()
    {
        if(_isInit)
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

            for (int i =0; i< exercises.Count; i++)
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
    public string ExerciceLetters => ExerciceSelected?.Letters?? "Exercice Letters";



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
        if(_typingExerciceEngine == null)
            return;
        await _navigation.NavigateToTypingExerciseAsync();
    }
}
