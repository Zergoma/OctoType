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
            if (IsStatic)
            {
                // TODO
                // dev mode -> need to do better
                return _exercice.Static!.Variants[0].Configuration.AllowedLetters;
            }

            // TODO
            // dev mode -> need to do better
            return _exercice.Dynamic!.Configurations[0].AllowedLetters;

        }
    }

    public bool IsStatic => _exercice.Static != null;
    public bool IsDynamic => _exercice.Dynamic != null;


}
