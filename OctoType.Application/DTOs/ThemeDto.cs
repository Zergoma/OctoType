namespace OctoType.Application.DTOs;

public class ThemeDto
{
    public string Name { get; set; }

    public ThemeStateDto Pending { get; set; }
    public ThemeStateDto Current { get; set; }
    public ThemeStateDto Correct { get; set; }
    public ThemeStateDto CorrectWithError { get; set; }
    public ThemeStateDto CurrentWrong { get; set; }
}

public class ThemeStateDto
{
    public string TextColor { get; set; }
    public string BackgroundColor { get; set; }
    public string BorderColor { get; set; }
    public int BorderThickness { get; set; }
}