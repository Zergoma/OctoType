namespace OctoType.Models.UI.Typing;

public sealed class TypingStyle
{
    public string TextColor { get; set; } = string.Empty;

    public string BackgroundColor { get; set; } = string.Empty;

    public string BorderColor { get; set; } = string.Empty;

    public int BorderThickness { get; set; } = 0;

    public Color GetTextColor()
    {
        return Color.FromArgb(TextColor);
    }

    public Color GetBackgroundColor()
    {
        return Color.FromArgb(BackgroundColor);
    }

    public Color GetBorderColor()
    {
        return Color.FromArgb(BorderColor);
    }
}