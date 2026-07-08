using OctoType.Application.Models.Themes;

namespace OctoType.Application.Interfaces
{
    public interface IChartResponseTimeColorsProvider
    {
        string GetHexColorTimeResponse(double timeInSecondes, ThemeState themeState);
        public string GetHexColorBg(ThemeState themeState);
        public string GetHexColorTxtLabel(ThemeState themeState);
    }
}