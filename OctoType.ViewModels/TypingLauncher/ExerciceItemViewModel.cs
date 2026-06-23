using CommunityToolkit.Mvvm.ComponentModel;

using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.ViewModels.TypingLauncher;

public partial class ExerciceItemViewModel : ObservableObject
{
    private readonly TypingExercise _exercice;
    private readonly int _idx;
    public ExerciceItemViewModel(TypingExercise exercice, int idx)
    {
        _exercice = exercice;
        _idx = idx;
        IsSelected = false;
    }

    public int Idx => _idx;

    [ObservableProperty] public partial bool IsSelected { get; set; }
     
    public string Name => _exercice.Name;
    public string Desciption => _exercice.Description;


    public string Letters
    {
        get
        {
                // TODO
                // dev mode -> need to do better
                return _exercice.ExerciceConfigs[0].TextData.AllowedLetters;
        }
    }

    // TODO
    // dev mode -> need to do better
    public bool IsStatic => _exercice.ExerciceConfigs[0].TextData.StaticTextData != null;

    // TODO
    // dev mode -> need to do better
    public bool IsDynamic => _exercice.ExerciceConfigs[0].TextData.DynamicTextData != null;


}
