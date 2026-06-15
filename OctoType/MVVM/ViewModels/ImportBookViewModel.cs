using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Domain.Models;

namespace OctoType.MVVM.ViewModels;

public partial class ImportBookViewModel : ObservableObject
{

    private readonly IChoosePath _choosePathPresenter;
    private readonly IImportFilePathProvider _importFilePahtProvider;
    private readonly IFileCopyProvider _fileSaverProvider;

    public ImportBookViewModel(
        IChoosePath choosePathPresnter,
        IImportFilePathProvider importFilePahtProvider,
        IFileCopyProvider fileSaverProvider)
    {
        _choosePathPresenter = choosePathPresnter;
        _importFilePahtProvider = importFilePahtProvider;
        _fileSaverProvider = fileSaverProvider;
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
    public async Task ImportFile()
    {
        string filename = Path.GetFileName(ImportFilePath);
        string folderDir = _importFilePahtProvider.ImportDirectory;
        string dstFile = Path.Combine(folderDir, filename);

        var copyResu =
            await _fileSaverProvider.CopyFileToAsync(ImportFilePath, dstFile, true);
    }
}
