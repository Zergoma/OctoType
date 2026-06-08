using OctoType.Application.Interfaces;

namespace OctoType.Infrastructure.Providers;

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
                "OctoType",
                "import");
}