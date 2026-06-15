using OctoType.Application.Interfaces;
using OctoType.Application.Models;
using OctoType.Domain.Entities;
using OctoType.Domain.Enums;
using OctoType.Domain.Models;

namespace OctoType.Application.Services;

public sealed class KeyboardAnalyzerService : IKeyboardAnalyzerService
{
    public Result<UnitTextAnalysis> Analyze(
        string text,
        IReadOnlyDictionary<char, KeyInfo> map)
    {
        KeyboardRow rowMask = KeyboardRow.None;
        Finger fingerMask = Finger.None;
        bool externalAccent = false;

        int leftCount = 0;
        int rightCount = 0;

        foreach (char c in text)
        {
            if (!map.TryGetValue(c, out KeyInfo info))
            {
                // character not in the map
                // this word is not possible with that layout
                return Result<UnitTextAnalysis>.Fail($"The character {c} have no correspondance");
            }

            rowMask |= info.Row;
            fingerMask |= info.Finger;

            externalAccent = info.ExtrenalAccent;

            if (IsLeftFinger(info.Finger))
            {
                leftCount++;
            }
            else
            {
                rightCount++;
            }
        }

        return Result<UnitTextAnalysis>.Ok(new ()
        {
            RowMask = rowMask,
            FingerMask = fingerMask,
            ExternalAccent = externalAccent,
            UsesLeftHand = leftCount > 0,
            UsesRightHand = rightCount > 0
        });
    }

    private static bool IsLeftFinger(Finger finger)
    {
        return finger is
            Finger.LeftPinky or
            Finger.LeftRing or
            Finger.LeftMiddle or
            Finger.LeftIndex;
    }
}