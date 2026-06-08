using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Domain.Constaintes;
using OctoType.Domain.Enums;


namespace OctoType.MVVM.ViewModels;


public partial class ImportWordViewModel : ObservableObject
{
    private readonly IChoosePath _choosePathPresenter;
    private readonly IWordImportServiceOrchestrator _wordImportService;

    public ImportWordViewModel(
        IChoosePath choosePathPresnter,
        IWordImportServiceOrchestrator wordImportService)
    {
        _choosePathPresenter = choosePathPresnter;
        _wordImportService = wordImportService;
    }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFileSelected))]
    public partial string ImportFilePath { get; set; }


    public bool IsFileSelected => !string.IsNullOrWhiteSpace(ImportFilePath) && File.Exists(ImportFilePath);



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
        await _wordImportService.ImportAsync(ImportFilePath, LanguageCodes.French, KeyboardLayout.AzertyFr);
    }
}
