using XyloType.Application.Models.Themes;

namespace XyloType.Application.Interfaces
{
    public interface IChartErrorProvider
    {
        string GetHexColorError(double percentFaile, ThemeState themeState);
    }
}