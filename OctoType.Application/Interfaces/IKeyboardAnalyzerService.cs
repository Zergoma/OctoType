using OctoType.Domain.Entities;
using OctoType.Domain.Enums;

namespace OctoType.Application.Interfaces;

public interface IKeyboardAnalyzerService
{
    WordAnalysis? Analyze(string text, KeyboardLayout layout);
}
