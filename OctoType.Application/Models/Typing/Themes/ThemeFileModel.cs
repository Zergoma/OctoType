namespace OctoType.Application.Models.Typing.Themes;

// TODO
// move to infrastructure layer
public class ThemeFileModel
{
    public string Name { get; set; }
    public ThemeStateFileModel Pending { get; set; } = new();
    public ThemeStateFileModel Current { get; set; } = new();
    public ThemeStateFileModel CurrentWrong { get; set; } = new();
    public ThemeStateFileModel Correct { get; set; } = new();
    public ThemeStateFileModel CorrectWithError { get; set; } = new();
    public ThemeStateFileModel Wrong { get; set; } = new();
}
