namespace XyloType.Application.Models.Typing.Themes;

public sealed class TypingStyle
{
    public string TextColor { get; set; } = string.Empty;

    public string BackgroundColor { get; set; } = string.Empty;

    public string BorderColor { get; set; } = string.Empty;

    public int BorderThickness { get; set; } = 0;
}