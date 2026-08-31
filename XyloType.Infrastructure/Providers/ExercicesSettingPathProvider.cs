using XyloType.Application.Interfaces;
using XyloType.Domain.Constaintes;

namespace XyloType.Infrastructure.Providers;

public class ExercicesSettingPathProvider : IExercicesSettingPathProvider
{
    public ExercicesSettingPathProvider()
    {
        // auto create the user specific directory
        Directory.CreateDirectory(ExerciceSettingPath());
    }

    public string ExerciceSettingPath()
        => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                AppNameData.AppName,
                "exercices_settings");
}