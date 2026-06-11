using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Application.ValueObjects;

namespace OctoType.MVVM.ViewModels;

public partial class ExerciceGeneratorViewModel : ObservableObject
{
    private readonly IPseudoWordBatchGenerator _pseudoWordBatchOrchestrator;
    public ExerciceGeneratorViewModel(IPseudoWordBatchGenerator pseudoWordGeneratorService)
    {
        _pseudoWordBatchOrchestrator = pseudoWordGeneratorService;
        AllowedChars = "abcdefghijklmnopqrstuvwxyz";
        NumberWords = 10;
        MinLengthWord = 3;
        MaxLengthWord = 3;
    }

    [ObservableProperty]
    public partial string GeneratedText{ get; set; }


    [ObservableProperty]
    public partial string AllowedChars { get; set; }


    public int NumberWords
    { 
        get;
        set
        {
            if (value == field)
                return;

            int clampValue = Math.Clamp(value, 1, 100);

            field = clampValue;
            OnPropertyChanged(nameof(NumberWords));
        }
    }

    public int MinLengthWord
    {
        get;
        set
        {
            if (value == field)
                return;

            int clampValue = Math.Clamp(value, 1, 100);

            field = clampValue;
            OnPropertyChanged(nameof(MinLengthWord));
        }
    }

    public int MaxLengthWord
    {
        get;
        set
        {
            if (value == field)
                return;

            int clampValue = Math.Clamp(value, 1, 100);

            field = clampValue;
            OnPropertyChanged(nameof(MaxLengthWord));
        }
    }


    [ObservableProperty]
    public partial string ErrorGeneratedTxt { get; set; } = string.Empty;

    [RelayCommand]
    public async Task GenerateWords()
    {
        Result<List<string>> resu = 
            _pseudoWordBatchOrchestrator.Generate(NumberWords, new PseudoWordOptions(AllowedChars, MinLengthWord, MaxLengthWord));

        if(resu.Success)
        {
            GeneratedText = string.Join(" ", resu.Value!);
            ErrorGeneratedTxt = "";
        }
        else
        {
            ErrorGeneratedTxt = resu.Error;
            GeneratedText = "";
            return;
        }
    }
}
