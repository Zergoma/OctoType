using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

        TypingExercices? exercicesLoaded =
            await _typingExerciceStorage.LoadAsync();

        if (exercicesLoaded is not null)
        {
            AllExercice.Clear();
            
            for(int i =0; i< exercicesLoaded.Exercices.Count; i++)// (TypingExercise item in exercicesLoaded.Exercices)
            {
                var item = exercicesLoaded.Exercices[i];
                AllExercice.Add(new ExerciceItemViewModel(item, i));
            }
        }
    }


    public bool IsSelectedExercice => ExerciceSelected != null;
    public bool IsNoSelection => !IsSelectedExercice;

    public int IdxSelected => ExerciceSelected?.Idx ?? -1;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSelectedExercice))]
    [NotifyPropertyChangedFor(nameof(IsNoSelection))]
    public partial ExerciceItemViewModel? ExerciceSelected { get; set; }

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
