using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Models;
using OctoType.Application.UseCases;
using OctoType.Application.ValueObjects;

namespace OctoType.MVVM.ViewModels;

public partial class ExerciceGeneratorViewModel : ObservableObject
{
    private readonly IPseudoWordBatchGenerator _pseudoWordBatchGenerator;
    private readonly ITypingExercicesManager _typingExerciceManager;
    private readonly ITypingExercicesStorage _typingExercicePersistence;
    private readonly ISaveTypingExerciceUseCase _saveUseCase;

    private readonly List<string> _languageAvailableElem;
    private readonly List<KeyBoardLayoutDto> _keyboardLayoutAvailableElem;

    public ExerciceGeneratorViewModel(
        IPseudoWordBatchGenerator pseudoWordBatchGenerator,
        ILanguageAvailableService languageAvailableService,
        IKeyBoardLayoutAvailableService keyboardLayoutAvailableService,
        ITypingExercicesManager typingExerciceManager,
        ITypingExercicesStorage typingExercicePersistence,
        ISaveTypingExerciceUseCase saveUseCase)
    {
        _pseudoWordBatchGenerator = pseudoWordBatchGenerator;
        AllowedChars = "abcdefghijklmnopqrstuvwxyz";
        NumberWords = 10;
        MinLengthWord = 3;
        MaxLengthWord = 3;
        _languageAvailableElem = languageAvailableService.GetAvailableLanguage();
        _keyboardLayoutAvailableElem = keyboardLayoutAvailableService.GetKeyBoardAvailable();

        _typingExerciceManager = typingExerciceManager;
        _typingExercicePersistence = typingExercicePersistence;
        _saveUseCase = saveUseCase;
    }

    public async Task InitializeAsync()
    {
        TypingExercices? exercicesLoaded =
            await _typingExercicePersistence.LoadAsync();

        if (exercicesLoaded is not null)
        {
            _typingExerciceManager.Exercice = exercicesLoaded;
        }
    }

    public IReadOnlyList<string> LanguageAvailable => _languageAvailableElem;
    public string? LanguageSelected { get; set; }

    public IReadOnlyList<KeyBoardLayoutDto> KeyboardLayoutAvailable => _keyboardLayoutAvailableElem;
    public KeyBoardLayoutDto? KeyboardLayoutSelected { get; set; }


    [ObservableProperty] public partial string GeneratedText { get; set; }

    [ObservableProperty] public partial string ExerciceName { get; set; }

    [ObservableProperty] public partial string Description { get; set; }

    [ObservableProperty] public partial string AllowedChars { get; set; }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StaticDynamicText))]
    public partial bool IsStaticGenerated { get; set; } = true;

    public string StaticDynamicText => IsStaticGenerated ? "Static" : "Dynamic";

    public int NumberWords
    {
        get;
        set
        {
            if (value == field)
                return;

            int clampValue = Math.Clamp(value, 1, 100);

            field = clampValue;
            OnPropertyChanged();
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
            OnPropertyChanged();
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
            OnPropertyChanged();
        }
    }

    [ObservableProperty] public partial string ErrorMessageTxt { get; set; } = string.Empty;
    [ObservableProperty] public partial string SuccessMessageTxt { get; set; } = string.Empty;

    [RelayCommand]
    private void GenerateWords()
    {
        Result<List<string>> resu =
            _pseudoWordBatchGenerator.Generate(
                NumberWords,
                new PseudoWordOptions(AllowedChars, MinLengthWord, MaxLengthWord));

        if (resu.Success)
        {
            GeneratedText = string.Join(" ", resu.GetValue);
            ErrorMessageTxt = "";
        }
        else
        {
            ErrorMessageTxt = resu.Error;
            GeneratedText = "";
        }
    }

    [RelayCommand]
    private void SwitchStaticDynamic()
    {
        IsStaticGenerated = !IsStaticGenerated;
    }

    [RelayCommand]
    private async Task SaveNewExerciceSetting()
    {
        ErrorMessageTxt = string.Empty;
        SuccessMessageTxt = string.Empty;

        if (KeyboardLayoutSelected is null)
        {
            ErrorMessageTxt = "You need to select a keyboard";
            return;
        }

        TypingExerciseCreateParameters settingbase = new() 
        {
            Name = ExerciceName,
            Description = Description,
            Language = LanguageSelected,
            KeyBoardLayoutDto = KeyboardLayoutSelected,
            AllowedLetters = AllowedChars
        };


        var resu = 
            await _saveUseCase.ExecuteAsync(
                settingbase,
                IsStaticGenerated,
                GeneratedText,
                _typingExerciceManager);

        if (!resu.Success)
        {
            ErrorMessageTxt = resu.Error;
        }
        else
        {
            SuccessMessageTxt = "Exercice succesfully saved !";
        }
    }
}