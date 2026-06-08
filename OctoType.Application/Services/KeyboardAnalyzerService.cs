using OctoType.Application.Interfaces;
using OctoType.Domain.Entities;
using OctoType.Domain.Enums;
using OctoType.Domain.Models;

namespace OctoType.Application.Services;

public sealed class KeyboardAnalyzerService : IKeyboardAnalyzerService
{
    private readonly IKeyboardKeyLocatorManager _keyboardKeyLocatorManager;


    public KeyboardAnalyzerService(
        IKeyboardKeyLocatorManager keyboardKeyLocatorManager)
    {
        _keyboardKeyLocatorManager = keyboardKeyLocatorManager;
    }

    public WordAnalysis? Analyze(string text, KeyboardLayout layout)
    {
        IKeyboardKeyLocator? keyBoardLocator =
            _keyboardKeyLocatorManager.GetKeyBoardKeyLocator(layout);

        if (keyBoardLocator == null)
        {
            throw new NotSupportedException($"Layout not supported: {layout}");
        }

        return AnalyzeInternal(text, keyBoardLocator.KeyLocator, keyBoardLocator.GetKeyboardType);
    }

    private static WordAnalysis? AnalyzeInternal(
        string text,
        IReadOnlyDictionary<char, KeyInfo> map,
        KeyboardLayout layout)
    {
        KeyboardRow rowMask = KeyboardRow.None;
        Finger fingerMask = Finger.None;

        int leftCount = 0;
        int rightCount = 0;

        foreach (char c in text)
        {
            if (!map.TryGetValue(c, out KeyInfo info))
            {
                // character not in the map
                // this word is not possible with that layout
                return null;
            }

            rowMask |= info.Row;
            fingerMask |= info.Finger;

            if (IsLeftFinger(info.Finger))
            {
                leftCount++;
            }
            else
            {
                rightCount++;
            }
        }

        return new ()
        {
            Layout = layout,
            RowMask = rowMask,
            FingerMask = fingerMask,
            UsesLeftHand = leftCount > 0,
            UsesRightHand = rightCount > 0
        };
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