using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.ViewModels.TypingLauncher;

public partial class TypingLauncherViewModel : ObservableObject
{
    private readonly ITypingExercicesStorage _typingExerciceStorage;
    public ObservableCollection<ExerciceItemViewModel> AllExercice { get; set; } = [];
    private bool _isInit = false;

    public TypingLauncherViewModel(
        ITypingExercicesStorage typingExerciceStorage)
    {
        _typingExerciceStorage = typingExerciceStorage;
    }

    public async Task Initilization()
    {
        if(_isInit)
        {
            return;
        }
        _isInit = true;

        Result<TypingExercices> exercicesLoadedResult =
            await _typingExerciceStorage.LoadAsync();

        if (exercicesLoadedResult.Success)
        {
            AllExercice.Clear();

            List<TypingExercise> exercises = exercicesLoadedResult.GetValue.Exercices;

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

    }

}
