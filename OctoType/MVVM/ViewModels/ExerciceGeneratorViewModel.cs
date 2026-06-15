using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Models;
using OctoType.Application.ValueObjects;

namespace OctoType.MVVM.ViewModels;

public partial class ExerciceGeneratorViewModel : ObservableObject
{
    private readonly IPseudoWordListGenerator _pseudoWordBatchOrchestrator;
    private readonly ILanguageAvailableService _languageAvailableService;
    private readonly IKeyBoardLayoutAvailableService _keyboardLayoutAvailableService;
    private readonly IExerciseSettingsStore _exerciceStrore;
    private readonly IExercicesSettingPathProvider _exercicePathProvider;


    //private readonly IKeyboardAnalyzerService _keyboardAnalyzerService;
    public ExerciceGeneratorViewModel(
        IPseudoWordListGenerator pseudoWordGeneratorService,
        ILanguageAvailableService languageAvailableService,
        IKeyBoardLayoutAvailableService keyboardLayoutAvailableService,
        IExerciseSettingsStore exerciceStrore,
        IExercicesSettingPathProvider exercicePathProvider)
    //IKeyboardAnalyzerService keyboardAnalyzerService)
    {
        _pseudoWordBatchOrchestrator = pseudoWordGeneratorService;
        AllowedChars = "abcdefghijklmnopqrstuvwxyz";
        NumberWords = 10;
        MinLengthWord = 3;
        MaxLengthWord = 3;
        _languageAvailableService = languageAvailableService;
        _keyboardLayoutAvailableService = keyboardLayoutAvailableService;
        _exerciceStrore = exerciceStrore;
        _exercicePathProvider = exercicePathProvider;
        //_keyboardAnalyzerService = keyboardAnalyzerService;
    }

    public List<string> LanguageAvailable => _languageAvailableService.GetAvailableLanguage();
    public object? LanguageSelected { get; set; }

    public List<KeyBoardLayoutDto> KeyboardLayoutAvailable => [.. _keyboardLayoutAvailableService.GetKeyBoardAvailable()];
    public object? KeyboardLayoutSelected { get; set; }


    [ObservableProperty]
    public partial string GeneratedText { get; set; }


    [ObservableProperty]
    public partial string FileName { get; set; }
    [ObservableProperty]
    public partial string ShortDescription { get; set; }

    [ObservableProperty]
    public partial string Description { get; set; }

    [ObservableProperty]
    public partial string AllowedChars { get; set; }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StaticDynamicText))]
    public partial bool IsStaticGenerated { get; set; } = true;

    public string StaticDynamicText
    {
        get
        {
            if (IsStaticGenerated)
            {
                return "Static";
            }
            return "Dynamic";
        }
    }

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

    [ObservableProperty]
    public partial string ErrorGeneratedTxt { get; set; } = string.Empty;

    [RelayCommand]
    private void GenerateWords()
    {
        Result<List<string>> resu =
            _pseudoWordBatchOrchestrator.Generate(NumberWords, new PseudoWordOptions(AllowedChars, MinLengthWord, MaxLengthWord));

        if (resu.Success)
        {
            GeneratedText = string.Join(" ", resu.GetValue);
            ErrorGeneratedTxt = "";
        }
        else
        {
            ErrorGeneratedTxt = resu.Error;
            GeneratedText = "";
        }
    }

    [RelayCommand]
    private void SwitchStaticDynamic()
    {
        IsStaticGenerated = !IsStaticGenerated;
    }

    [RelayCommand]
    private async Task SaveExercideSetting()
    {
        ErrorGeneratedTxt = string.Empty;

        if (string.IsNullOrWhiteSpace(AllowedChars))
        {
            ErrorGeneratedTxt = "No letters selected";
            return;
        }

        if(string.IsNullOrWhiteSpace(FileName))
        {
            ErrorGeneratedTxt = "You need to set the filename";
            return;
        }

        bool isStatic = IsStaticGenerated;

        TypingExerciceSetting newSeetings = new()
        {
            Name = ShortDescription,
            Description = Description,
        };

        if (LanguageSelected is string language)
        {
            newSeetings.Language = language;
        }


        if (KeyboardLayoutSelected is KeyBoardLayoutDto keyboard)
        {
            AllowLetter allowLetters = new()
            {
                KeyboardLayout = keyboard,
                Letters = AllowedChars
            };

            if (isStatic)
            {
                TypingExerciseSettingStatic staticsetting = new()
                {
                    AllowLettersConfig = [allowLetters],
                    Text = GeneratedText
                };
                newSeetings.StaticSettings = staticsetting;
            }
            else
            {
                TypingExerciceSettingDynamic dynamicSetting = new()
                {
                    AllowLettersConfig = [allowLetters]
                };
                newSeetings.DynamicSettings = dynamicSetting;
            }
        }
        else
        {
            ErrorGeneratedTxt = "You need to select a keybord";
            return;
        }

        string fileName = FileName;
        if(!FileName.Contains(".json", StringComparison.CurrentCultureIgnoreCase))
        {
            fileName = $"{fileName}.json";
        }

        string exerciceFolder = _exercicePathProvider.ExerciceSettingPath();
        string fullPath = Path.Combine(
            exerciceFolder,
            fileName);
        await _exerciceStrore.SaveAsync(newSeetings, fullPath);
    }
}
