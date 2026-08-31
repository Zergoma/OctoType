namespace XyloType.Infrastructure.Theme.Models;

public class ThemeStateFileModel
{
    public string TextColorLight { get; set; } = string.Empty;
    public string BackgroundColorLight { get; set; } = string.Empty;
    public string BorderColorLight { get; set; } = string.Empty;
    public int BorderThicknessLight { get; set; }


    public string TextColorDark { get; set; } = string.Empty;
    public string BackgroundColorDark { get; set; } = string.Empty;
    public string BorderColorDark { get; set; } = string.Empty;
    public int BorderThicknessDark { get; set; }
}