namespace OctoType.Infrastructure.Theme.Models;

public class ThemeFileModel
{
    public string Name { get; set; } = string.Empty;
    public ThemeStateFileModel Pending { get; set; } = new();
    public ThemeStateFileModel Current { get; set; } = new();
    public ThemeStateFileModel CurrentWrong { get; set; } = new();
    public ThemeStateFileModel Correct { get; set; } = new();
    public ThemeStateFileModel CorrectWithError { get; set; } = new();
    public ThemeStateFileModel Wrong { get; set; } = new();
}
