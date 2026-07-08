using OctoType.Application.Models.Themes;

namespace OctoType.Application.Interfaces
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