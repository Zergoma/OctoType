using XyloType.Domain.Entities;

namespace XyloType.Domain.Models;

public class AzertKeyLocatorsBuilder
{
    public static Dictionary<char, KeyInfo> BuildMap()
    {
        return new Dictionary<char, KeyInfo>
        {
            // =========================
            // ROW B (AZERTYUIOP)
            // =========================

            ['a'] = new KeyInfo(KeyboardRow.B, Finger.LeftPinky),
            ['z'] = new KeyInfo(KeyboardRow.B, Finger.LeftRing),
            ['e'] = new KeyInfo(KeyboardRow.B, Finger.LeftMiddle),
            ['r'] = new KeyInfo(KeyboardRow.B, Finger.LeftIndex),
            ['t'] = new KeyInfo(KeyboardRow.B, Finger.LeftIndex),

            ['y'] = new KeyInfo(KeyboardRow.B, Finger.RightIndex),
            ['u'] = new KeyInfo(KeyboardRow.B, Finger.RightIndex),
            ['i'] = new KeyInfo(KeyboardRow.B, Finger.RightMiddle),
            ['o'] = new KeyInfo(KeyboardRow.B, Finger.RightRing),
            ['p'] = new KeyInfo(KeyboardRow.B, Finger.RightPinky),

            // =========================
            // ROW C (QSDFGHJKLM)
            // =========================

            ['q'] = new KeyInfo(KeyboardRow.C, Finger.LeftPinky),
            ['s'] = new KeyInfo(KeyboardRow.C, Finger.LeftRing),
            ['d'] = new KeyInfo(KeyboardRow.C, Finger.LeftMiddle),
            ['f'] = new KeyInfo(KeyboardRow.C, Finger.LeftIndex),
            ['g'] = new KeyInfo(KeyboardRow.C, Finger.LeftIndex),

            ['h'] = new KeyInfo(KeyboardRow.C, Finger.RightIndex),
            ['j'] = new KeyInfo(KeyboardRow.C, Finger.RightIndex),
            ['k'] = new KeyInfo(KeyboardRow.C, Finger.RightMiddle),
            ['l'] = new KeyInfo(KeyboardRow.C, Finger.RightRing),
            ['m'] = new KeyInfo(KeyboardRow.C, Finger.RightPinky),

            // =========================
            // ROW D (WXCVBN)
            // =========================

            ['w'] = new KeyInfo(KeyboardRow.D, Finger.LeftPinky),
            ['x'] = new KeyInfo(KeyboardRow.D, Finger.LeftRing),
            ['c'] = new KeyInfo(KeyboardRow.D, Finger.LeftMiddle),
            ['v'] = new KeyInfo(KeyboardRow.D, Finger.LeftIndex),

            ['b'] = new KeyInfo(KeyboardRow.D, Finger.RightIndex),
            ['n'] = new KeyInfo(KeyboardRow.D, Finger.RightMiddle),

            // =========================
            // ROW A (numbers / symbols simplifié)
            // =========================

            ['1'] = new KeyInfo(KeyboardRow.A, Finger.LeftPinky),
            ['2'] = new KeyInfo(KeyboardRow.A, Finger.LeftRing),
            ['3'] = new KeyInfo(KeyboardRow.A, Finger.LeftMiddle),
            ['4'] = new KeyInfo(KeyboardRow.A, Finger.LeftIndex),
            ['5'] = new KeyInfo(KeyboardRow.A, Finger.LeftIndex),

            ['6'] = new KeyInfo(KeyboardRow.A, Finger.RightIndex),
            ['7'] = new KeyInfo(KeyboardRow.A, Finger.RightIndex),
            ['8'] = new KeyInfo(KeyboardRow.A, Finger.RightMiddle),
            ['9'] = new KeyInfo(KeyboardRow.A, Finger.RightRing),
            ['0'] = new KeyInfo(KeyboardRow.A, Finger.RightPinky),

            // =========================
            // ACCENTS FR (IMPORTANT pour ton use case)
            // =========================

            ['é'] = new KeyInfo(KeyboardRow.B, Finger.RightIndex),
            ['è'] = new KeyInfo(KeyboardRow.C, Finger.RightRing),
            ['à'] = new KeyInfo(KeyboardRow.D, Finger.RightPinky),
            ['ç'] = new KeyInfo(KeyboardRow.D, Finger.LeftPinky),
            ['ù'] = new KeyInfo(KeyboardRow.C, Finger.RightPinky),

            ['ê'] = new KeyInfo(KeyboardRow.C, Finger.RightRing, true),
            ['â'] = new KeyInfo(KeyboardRow.B, Finger.LeftPinky, true),
            ['î'] = new KeyInfo(KeyboardRow.B, Finger.RightMiddle, true),
            ['ô'] = new KeyInfo(KeyboardRow.C, Finger.RightMiddle, true),
            ['û'] = new KeyInfo(KeyboardRow.C, Finger.RightPinky, true),

            // =========================
            // SPACE (optionnel mais utile pour stats)
            // =========================

            [' '] = new KeyInfo(KeyboardRow.None, Finger.LeftThumb | Finger.RightThumb),
        };
    }
}
