using OctoType.Application.Models.Themes;

namespace OctoType.Application.Interfaces
{
    public interface IChartErrorProvider
    {
        string GetHexColorError(double percentFaile, ThemeState themeState);
    }
}