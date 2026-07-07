using OctoType.Application.Models;

namespace OctoType.Application.Interfaces
{
    public interface IThemeChangerService
    {
        void SetDark();
        void SetLight();
        void SetToSystem();

        public IconeThemeState ApplyUserSelectedTheme();
    }
}