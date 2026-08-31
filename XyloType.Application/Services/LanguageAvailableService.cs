using XyloType.Application.Interfaces;
using XyloType.Domain.Constaintes;

namespace XyloType.Application.Services;

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
