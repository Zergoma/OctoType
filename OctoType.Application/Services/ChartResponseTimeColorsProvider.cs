using OctoType.Application.Interfaces;
using OctoType.Application.Models.Themes;

namespace OctoType.Application.Services;

public class ChartResponseTimeColorsProvider : IChartResponseTimeColorsProvider
{
    public string GetHexColorTimeResponse(double timeInSecondes, ThemeState themeState)
    {
        return themeState switch
        {
            ThemeState.Dark => timeInSecondes switch
            {
                < 1.0 => "#36B205",
                < 2.0 => "#91B200",
                < 3.0 => "#B25C00",
                < 4.0 => "#B21700",
                _ => "#444444"
            },
            _ => timeInSecondes switch
            {
                < 1.0 => "#42E506",
                < 2.0 => "#B5E200",
                < 3.0 => "#E07700",
                < 4.0 => "#DD2400",
                _ => "#000000"
            }
        };
    }

    public string GetHexColorBg(ThemeState themeState)
    {
        return themeState switch
        {
            ThemeState.Dark => "#000000",
            _ => "#FFFFFF",
        };
        
    }

    public string GetHexColorTxtLabel(ThemeState themeState)
    {
        return themeState switch
        {
            ThemeState.Dark => "#FFFFFF",
            _ => "#000000",
        };
    }
}
