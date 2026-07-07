using OctoType.Application.Models;

namespace OctoType.Application.Interfaces
{
    public interface IThemeIconeProvider
    {
        string GetIconeCode(IconeThemeState state);
    }
}