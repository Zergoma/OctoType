using XyloType.Application.Interfaces;
using XyloType.Domain.Constaintes;

namespace XyloType.Infrastructure.Providers;

public class ImportFilePathProvider : IImportFilePathProvider
{
    public ImportFilePathProvider()
    {
        // auto create the user specific directory
        Directory.CreateDirectory(ImportDirectory);
    }

    public string ImportDirectory
        => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                AppNameData.AppName,
                "import");
}