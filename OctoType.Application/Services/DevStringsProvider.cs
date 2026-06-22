using OctoType.Application.Interfaces;

namespace OctoType.Application.Services;

public class DevStringsProvider : IStringsProvider
{
    public async Task<Result<IEnumerable<string>>> GetStringsAsync()
    {
        IEnumerable<string> data =
        [
            "super↵",
            "re",
            "chaud",
            "boom↵",
            "si tu veux taper plus vite",
            "tu dois pour commencer améliorer ta précision",
            "un entraînement régulier permet de se perfectionner, chaque fois que tu referas cette leçon, tu amélioreras ta vitesse",
            "tata",
            "the quick brown fox jumps over the lazy dog",
            "octopus typing trainer",
            "maui is surprisingly fun",
            "cplusplus is still alive"
        ];

        return Result<IEnumerable<string>>.Ok(data);
    }
}
