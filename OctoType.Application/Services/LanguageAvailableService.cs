using OctoType.Application.Interfaces;
using OctoType.Domain.Constaintes;

namespace OctoType.Application.Services;

public class LanguageAvailableService : ILanguageAvailableService
{
    public List<string> GetAvailableLanguage()
    {
        return
        [
            LanguageCodes.French,
            LanguageCodes.English,
            LanguageCodes.German,
            LanguageCodes.Spanish,
            LanguageCodes.Italian
        ];
    }
}
