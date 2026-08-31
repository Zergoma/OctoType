using XyloType.Application;
using XyloType.Application.Models;
using XyloType.Domain.Models;

namespace XyloType.Application.Interfaces;

public interface IKeyboardAnalyzerService
{
    Result<UnitTextAnalysis> Analyze(string text, IReadOnlyDictionary<char, KeyInfo> map);
}
