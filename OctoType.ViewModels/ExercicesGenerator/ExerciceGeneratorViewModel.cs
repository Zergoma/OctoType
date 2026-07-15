using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.Application.Mappers;
using OctoType.Application.Models.Typing;
using OctoType.Application.Models.Typing.Exercices;
using OctoType.Application.UseCases;
using OctoType.Application.ValueObjects;

using OctoType.Domain.Typing;

namespace OctoType.ViewModels.ExercicesGenerator;

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
        ITypingExercicesManager typingExerciceManager,
        ITypingExercicesStorage typingExercicePersistence,
        ISaveTypingExerciceUseCase saveUseCase,

        IGenerationTypeSourceAvailableService generationTypeSource,
        IKeyBoardLayoutAvailableService keyboardLayoutAvailableService,
        ILanguageAvailableService languageAvailableService)
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

    private void SetKeyboardLayout(int id)
    {
        KeyBoardLayoutDto? itemKeyboard = _keyboardLayoutAvailableElem.Find(k => (int)k.KeyBoardCode == id);
        KeyboardLayoutSelected = itemKeyboard;
    }

    public async Task InitializeAsync(int keyboardLayoutDtoId)
    {
        SetKeyboardLayout(keyboardLayoutDtoId);
    }


    public string Title => ExerciceToUpdate == null ? "Générateur d'exercice" : "Éditer l'exercice";

    public IReadOnlyList<string> LanguageAvailable => _languageAvailableElem;
    public string? LanguageSelected { get; set; }

    public IReadOnlyList<KeyBoardLayoutDto> KeyboardLayoutAvailable => _keyboardLayoutAvailableElem;
    [ObservableProperty]
    public partial KeyBoardLayoutDto? KeyboardLayoutSelected { get; set; }


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

    private TypingExercise? ExerciceToUpdate { get; set; }
    public async Task<Result<bool>> InitFromExercice(Guid exerciceGuid, int keyboardLayoutDtoId)
    {
        SetKeyboardLayout(keyboardLayoutDtoId);

        // from keyboardlayout load correct exercice file
        if (KeyboardLayoutSelected is KeyBoardLayoutDto keyboardLayoutDto)
        {
            Result<TypingExercices> exercicesLoadedResult =
                await _typingExercicePersistence.LoadAsync(keyboardLayoutDto.KeyBoardCode);

            if (exercicesLoadedResult.Success)
            {
                _typingExerciceManager.Exercices = exercicesLoadedResult.GetValue;
            }
            else
            {
                return Result<bool>
                    .Fail(exercicesLoadedResult.Error);
            }
        }
        else
        {
            return Result<bool>
                .Fail("Not a correct keyboardLayoutDto");
        }

        // Get exercice
        TypingExercise? exercice = _typingExerciceManager.Exercices.Exercices.FirstOrDefault(x => x.Id == exerciceGuid);
        if (exercice == null)
            return Result<bool>
                .Fail("Exercice doesn't exist");

        // UI properties synchro
        ExerciceToUpdate = exercice;

        ExerciceName = ExerciceToUpdate.Name;
        Description = ExerciceToUpdate.Description;
        AllowedChars = ExerciceToUpdate.AllowedCharacters;


        if (ExerciceToUpdate.TextDataType is TypingTextDataStatic textDataStatic)
        {
            IsStaticGenerated = true;
            GeneratedText = textDataStatic.GeneratedText;
        }

        if (ExerciceToUpdate.TextDataType is TypingTextDataDynamic textDataDynamic)
        {
            IsStaticGenerated = false;
            MinLengthWord = textDataDynamic.LengthMin;
            MaxLengthWord = textDataDynamic.LengthMax;
            LanguageSelected = textDataDynamic.LanguagesSelected.FirstOrDefault();

            Result<GeneratedTypeSourceDto> generatedSourceResult = textDataDynamic.GeneratedTypeSource.ToDto();
            if (generatedSourceResult.Success)
            {
                GenerationTypeSourceSelected = generatedSourceResult.GetValue;
            }
            else
            {
                GenerationTypeSourceSelected = GeneratedTypeSourceDto.PseudoWords;
            }
        }
        return Result<bool>
            .Ok(true);
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

    public bool IsMessageVisible
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(ErrorMessageTxt) || !string.IsNullOrWhiteSpace(SuccessMessageTxt))
            {
                return true;
            }
            return false;
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsMessageVisible))]
    public partial string ErrorMessageTxt { get; set; } = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsMessageVisible))]
    public partial string SuccessMessageTxt { get; set; } = string.Empty;

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

    public bool IsSaveNewVisible => ExerciceToUpdate == null;
    [RelayCommand]
    private async Task SaveNewExerciceSetting()
    {
        if (ExerciceToUpdate != null)
            return;

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
                if (!generationTypeSourceMapResult.Success)
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


        if (KeyboardLayoutSelected is KeyBoardLayoutDto keyboardLayoutDto)
        {
            Result<TypingExercices> exercicesLoadedResult =
                await _typingExercicePersistence.LoadAsync(keyboardLayoutDto.KeyBoardCode);

            if (exercicesLoadedResult.Success)
            {
                _typingExerciceManager.Exercices = exercicesLoadedResult.GetValue;
            }
            else
            {
                // file not found, first time
                _typingExerciceManager.Exercices =
                    new()
                    {
                        KeyboardLayout = keyboardLayoutDto,
                    };
            }

            var resu =
                await _saveUseCase.SaveNewExerciceAsync(
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
                SuccessMessageTxt = "Exercices succesfully saved !";
            }
        }
    }


    public bool IsUpdateVisible => !IsSaveNewVisible;
    [RelayCommand]
    public async Task UpdateExerciceSetting()
    {
        if (ExerciceToUpdate == null)
            return;

        TypingExercise tempoExercice =
            new()
            {
                Id = ExerciceToUpdate.Id,
                Name = ExerciceName,
                Description = Description,
                AllowedCharacters = AllowedChars
            };


        if (IsStaticGenerated)
        {
            tempoExercice.TextDataType =
                new TypingTextDataStatic()
                {
                    GeneratedText = GeneratedText,
                };
        }
        else
        {
            Result<TypingTextDataDynamic> textDynamicResult = BuildFromUITextDataDynamic();
            if (!textDynamicResult.Success)
            {
                ErrorMessageTxt = textDynamicResult.Error;
                return;
            }
            tempoExercice.TextDataType = textDynamicResult.GetValue;
        }

        var resu =
                await _saveUseCase.UpdateExerciceAsync(
                    _typingExerciceManager,
                    tempoExercice);

        if (!resu.Success)
        {
            ErrorMessageTxt = resu.Error;
        }
        else
        {
            SuccessMessageTxt = "Exercices succesfully saved !";
        }

    }


    private Result<TypingTextDataDynamic> BuildFromUITextDataDynamic()
    {
        if (GenerationTypeSourceSelected is GeneratedTypeSourceDto generationTypeSourceDto)
        {
            Result<GeneratedTypeSource> generationTypeSourceMapResult = generationTypeSourceDto.ToModel();
            if (!generationTypeSourceMapResult.Success)
            {
                Result<TypingTextDataDynamic>
                    .Fail(generationTypeSourceMapResult.Error);
            }

            TypingTextDataDynamic dynamicTypingTextData =
                new()
                {
                    LengthMax = Math.Max(MaxLengthWord, MinLengthWord),
                    LengthMin = Math.Min(MaxLengthWord, MinLengthWord),
                    LanguagesSelected =
                        LanguageSelected != null
                        ? [LanguageSelected]
                        : [],
                    GeneratedTypeSource = generationTypeSourceMapResult.GetValue
                };

            return Result<TypingTextDataDynamic>
                .Ok(dynamicTypingTextData);
        }
        return Result<TypingTextDataDynamic>
                    .Fail("You need to select a Generation Type source");
    }
}