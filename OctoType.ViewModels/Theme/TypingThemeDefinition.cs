using OctoType.Application.Models.Typing.Themes;

namespace OctoType.ViewModels.Theme;

public class TypingThemeDefinition
{
    public string Name { get; set; }
    public TypingStyle Pending { get; set; }
}