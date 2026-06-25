using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Mappers;
using OctoType.Application.Models.Typing.Exercices;
using OctoType.Application.UseCases;
using OctoType.Application.ValueObjects;
using OctoType.Domain.Typing;

namespace OctoType.ViewModels.Exercices;

public partial class ExerciceGeneratorViewModel : ObservableObject
{
    private readonly IPseudoWordBatchGenerator _pseudoWordBatchGenerator;
    private readonly ITypingExercicesManager _typingExerciceManager;
    private readonly ITypingExercicesStorage _typingExercicePersistence;
    private readonly ISaveTypingExerciceUseCase _saveUseCase;

    private readonly List<string> _languageAvailableElem;
    private readonly List<KeyBoardLayoutDto> _keyboardLayoutAvailableElem;
    private readonly List<GeneratedTypeSourceDto> _generationTypeSourcAvailableElem;

    public ExerciceGeneratorViewModel(
        IPseudoWordBatchGenerator pseudoWordBatchGenerator,
        ILanguageAvailableService languageAvailableService,
        IKeyBoardLayoutAvailableService keyboardLayoutAvailableService,
        ITypingExercicesManager typingExerciceManager,
        ITypingExercicesStorage typingExercicePersistence,
        ISaveTypingExerciceUseCase saveUseCase,
        IGenerationTypeSourceAvailableService generationTypeSource)
    {
        _pseudoWordBatchGenerator = pseudoWordBatchGenerator;
        AllowedChars = "abcdefghijklmnopqrstuvwxyz";
        NumberWords = 10;
        MinLengthWord = 3;
        MaxLengthWord = 3;
        _languageAvailableElem = languageAvailableService.GetAvailableLanguage();
        _keyboardLayoutAvailableElem = keyboardLayoutAvailableService.GetKeyBoardAvailable();
        _generationTypeSourcAvailableElem = generationTypeSource.GetGenerationTypeSourceAvailable();

        _typingExerciceManager = typingExerciceManager;
        _typingExercicePersistence = typingExercicePersistence;
        _saveUseCase = saveUseCase;
    }

    public async Task InitializeAsync()
    {
        Result<TypingExercices> exercicesLoadedResult =
            await _typingExercicePersistence.LoadAsync();

        if (exercicesLoadedResult.Success)
        {
            _typingExerciceManager.Exercice = exercicesLoadedResult.GetValue;
        }
        else
        {
            _typingExerciceManager.Exercice = new();
        }
    }

    public IReadOnlyList<string> LanguageAvailable => _languageAvailableElem;
    public string? LanguageSelected { get; set; }

    public IReadOnlyList<KeyBoardLayoutDto> KeyboardLayoutAvailable => _keyboardLayoutAvailableElem;
    public KeyBoardLayoutDto? KeyboardLayoutSelected { get; set; }


    public IReadOnlyList<GeneratedTypeSourceDto> GenerationTypeSourceAvailable => _generationTypeSourcAvailableElem;
    public GeneratedTypeSourceDto? GenerationTypeSourceSelected { get; set; }


    [ObservableProperty] public partial string GeneratedText { get; set; } = string.Empty;

    // User can type text in the editor too
    // we add new letter in the allowed
    partial void OnGeneratedTextChanged(string value)
    {
        List<char> detectedChar = value
            .Where(c =>
                !char.IsWhiteSpace(c) &&
                !AllowedChars.Contains(c))
            .Distinct()
            .ToList();

        AllowedChars += string.Join(null, detectedChar);
    }

    [ObservableProperty] public partial string ExerciceName { get; set; } = string.Empty;

    [ObservableProperty] public partial string Description { get; set; } = string.Empty;

    [ObservableProperty] public partial string AllowedChars { get; set; } = string.Empty;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StaticDynamicText))]
    [NotifyPropertyChangedFor(nameof(IsDynamic))]
    public partial bool IsStaticGenerated { get; set; } = true;

    public bool IsDynamic => !IsStaticGenerated;

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

        string allowedCharsStrict = AllowedLettersExtractor.ExtractAllowedLetters(AllowedChars, GeneratedText);

        TypingExerciseCreateParameters settingbase = new()
        {
            Name = ExerciceName,
            Description = Description,
            Language = LanguageSelected,
            KeyBoardLayoutDto = KeyboardLayoutSelected,
            AllowedLetters = allowedCharsStrict
        };

        TypingTextDataDynamic? dynamicTypingTextData = null;

        if (!IsStaticGenerated)
        {
            if (GenerationTypeSourceSelected is GeneratedTypeSourceDto generationTypeSourceDto)
            {
                var generationTypeSourceMapResult = generationTypeSourceDto.ToModel();
                if(!generationTypeSourceMapResult.Success)
                {
                    ErrorMessageTxt = generationTypeSourceMapResult.Error;
                    return;
                }

                // TODO
                // creating dto for this ? (for GeneratedTypeSource, model not dto)
                dynamicTypingTextData = new()
                {
                    LengthMax = Math.Max(MaxLengthWord, MinLengthWord),
                    LengthMin = Math.Min(MaxLengthWord, MinLengthWord),
                    LanguagesSelected =
                        LanguageSelected != null
                        ? [LanguageSelected]
                        : [],
                    GeneratedTypeSource = generationTypeSourceMapResult.GetValue
                };
            }
            else
            {
                ErrorMessageTxt = "You need to select a Generation Type source";
                return;
            }
        }

        var resu =
            await _saveUseCase.ExecuteAsync(
                settingbase,
                IsStaticGenerated,
                GeneratedText,
                _typingExerciceManager,
                dynamicTypingTextData);

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