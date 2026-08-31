using XyloType.Application.Interfaces;
using XyloType.Application.Models.Themes;

namespace XyloType.Application.Services;

public class ChartErrorProvider : IChartErrorProvider
{
    public string GetHexColorError(double percentFaile, ThemeState themeState)
    {
        return themeState switch
        {
            ThemeState.Dark => percentFaile switch
            {
                < 5.0 => "#36B205",
                < 10.0 => "#52B205",
                < 15.0 => "#70B203",
                < 20.0 => "#91B200",
                < 25.0 => "#A4B100",
                < 30.0 => "#B2A700",
                < 35.0 => "#B29800",
                < 40.0 => "#B28700",
                < 45.0 => "#B27600",
                < 50.0 => "#B26600",
                < 55.0 => "#B25C00",
                < 60.0 => "#B24F00",
                < 65.0 => "#B24100",
                < 70.0 => "#B23200",
                < 75.0 => "#B22300",
                < 80.0 => "#B21700",
                < 85.0 => "#9A1200",
                < 90.0 => "#7F0E00",
                < 95.0 => "#620A00",
                < 100.0 => "#4A0600",
                _ => "#444444"
            },
            _ => percentFaile switch
            {
                < 5.0 => "#42E506",
                < 10.0 => "#63E505",
                < 15.0 => "#8AE403",
                < 20.0 => "#B5E200",
                < 25.0 => "#C9DF00",
                < 30.0 => "#D9D600",
                < 35.0 => "#E2C600",
                < 40.0 => "#E8B400",
                < 45.0 => "#E69A00",
                < 50.0 => "#E68700",
                < 55.0 => "#E07700",
                < 60.0 => "#E06400",
                < 65.0 => "#E04F00",
                < 70.0 => "#E03D00",
                < 75.0 => "#DF3000",
                < 80.0 => "#DD2400",
                < 85.0 => "#C81C00",
                < 90.0 => "#A81400",
                < 95.0 => "#840D00",
                < 100.0 => "#600700",
                _ => "#000000"
            }
        };
    }
}