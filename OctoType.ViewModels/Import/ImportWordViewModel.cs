using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;

namespace OctoType.ViewModels.Import;


public partial class ImportWordViewModel : ObservableObject
{
    private readonly IChoosePath _choosePathPresenter;
    private readonly IWordImportOrchestrator _wordImportOrchestrator;
    private readonly IKeyboardKeyLocatorManager _keyboardKeyLocatorManager;
    private readonly ILanguageAvailableService _languageAvailableService;
    private readonly IKeyBoardLayoutAvailableService _keyboardLayoutAvailableService;

    public ImportWordViewModel(
        IChoosePath choosePathPresnter,
        IWordImportOrchestrator wordImportService,
        IKeyboardKeyLocatorManager keyboardKeyLocatorManager,
        ILanguageAvailableService languageAvailableService,
        IKeyBoardLayoutAvailableService keyboardLayoutAvailableService)
    {
        _choosePathPresenter = choosePathPresnter;
        _wordImportOrchestrator = wordImportService;
        _keyboardKeyLocatorManager = keyboardKeyLocatorManager;
        _languageAvailableService = languageAvailableService;
        _keyboardLayoutAvailableService = keyboardLayoutAvailableService;
    }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFileSelected))]
    public partial string ImportFilePath { get; set; }

    public List<String> AllLanguage => _languageAvailableService.GetAvailableLanguage();

    public object? SelectedLanguage { get; set; } = null;

    public List<KeyBoardLayoutDto> KeyboardLayoutAvailable => [.. _keyboardLayoutAvailableService.GetKeyBoardAvailable()];
    public object? SelectedKeyboard { get; set; } = null;

    public bool IsFileSelected => !string.IsNullOrWhiteSpace(ImportFilePath) && File.Exists(ImportFilePath);

    [ObservableProperty]
    public partial string ErrorImport { get; set; } = string.Empty;


    [RelayCommand]
    public async Task SelectFile()
    {
        Result<string?> resuPath =
            await _choosePathPresenter.SelectPathAsync();

        if (!resuPath.Success)
            return;

        if (resuPath.Value is null)
            return;

        string srcFile = resuPath.Value;

        if (!File.Exists(srcFile))
            return;

        ImportFilePath = srcFile;
    }

    [RelayCommand]
    public async Task ImportWordsFromFile()
    {
        ErrorImport = string.Empty;

        if (SelectedLanguage is null)
        {
            ErrorImport = "Select a language first";
            return;
        }

        if (SelectedKeyboard is null)
        {
            ErrorImport = "Select a keyboard";
            return;
        }


        if (SelectedLanguage is string language &&
            SelectedKeyboard is KeyBoardLayoutDto keyboard)
        {
            Result<IKeyboardKeysLocator> keyBoardLocatorResult =
                _keyboardKeyLocatorManager.GetKeyBoardKeyLocator(keyboard);

            if (!keyBoardLocatorResult.Success)
            {
                ErrorImport = keyBoardLocatorResult.Error;
                return;
            }

            IKeyboardKeysLocator keyBoardLocator = keyBoardLocatorResult.GetValue;

            Result<bool> resuImport =
                await _wordImportOrchestrator.ImportAsync(ImportFilePath, language, keyBoardLocator);

            if (resuImport.Success)
            {
                ErrorImport = "";
            }
            else
            {
                ErrorImport = resuImport.Error;
            }
        }
        else
        {
            ErrorImport = "Select a language first";
        }
    }
}
