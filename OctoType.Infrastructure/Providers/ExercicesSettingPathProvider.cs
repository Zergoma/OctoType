using OctoType.Application.Interfaces;

namespace OctoType.Infrastructure.Providers;

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
                "OctoType",
                "exercices_settings");
}