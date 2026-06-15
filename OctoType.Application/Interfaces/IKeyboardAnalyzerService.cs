using OctoType.Application.Models;
using OctoType.Domain.Models;

namespace OctoType.Application.Interfaces;

public interface IKeyboardAnalyzerService
{
    Result<UnitTextAnalysis> Analyze(string text, IReadOnlyDictionary<char, KeyInfo> map);
}
