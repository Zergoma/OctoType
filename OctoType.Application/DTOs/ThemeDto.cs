namespace OctoType.Application.DTOs;

public class ThemeDto
{
    public string Name { get; set; } = string.Empty;

    public required ThemeStateDto Pending { get; set; }
    public required ThemeStateDto Current { get; set; }
    public required ThemeStateDto Correct { get; set; }
    public required ThemeStateDto CorrectWithError { get; set; }
    public required ThemeStateDto CurrentWrong { get; set; }
}

public class ThemeStateDto
{
    public string TextColor { get; set; } = string.Empty;
    public string BackgroundColor { get; set; } = string.Empty;
    public string BorderColor { get; set; } = string.Empty;
    public int BorderThickness { get; set; }
}