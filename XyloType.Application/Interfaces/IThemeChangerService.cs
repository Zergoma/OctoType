using XyloType.Application.Models.Themes;

namespace XyloType.Application.Interfaces
{
    public interface IThemeChangerService
    {
        void SetDark();
        void SetLight();
        void SetToSystem();

        ThemeStateConfiguration ApplyUserSelectedTheme();

        ThemeState GetTheme();
    }
}