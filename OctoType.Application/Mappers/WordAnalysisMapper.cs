using OctoType.Application.Models;
using OctoType.Domain.Entities;
using OctoType.Domain.Enums;

namespace OctoType.Application.Mappers;

static public class WordAnalysisMapper
{
    static public WordAnalysis ToEntity(this UnitTextAnalysis unitTextAnalysis, KeyboardLayout layout)
    {
        return new()
        {
            Layout = layout,
            UsesLeftHand = unitTextAnalysis.UsesLeftHand,
            UsesRightHand = unitTextAnalysis.UsesRightHand,
            RowMask = unitTextAnalysis.RowMask,
            FingerMask = unitTextAnalysis.FingerMask,
            ExternalAccent = unitTextAnalysis.ExternalAccent,
        };
    }
}
