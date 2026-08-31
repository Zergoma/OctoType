namespace XyloType.Domain.Entities;

[Flags]
public enum Finger
{
    None = 0,

    LeftPinky = 1 << 9,     // 10000 00000
    LeftRing = 1 << 8,      // 01000 00000
    LeftMiddle = 1 << 7,    // 00100 00000
    LeftIndex = 1 << 6,     // 00010 00000
    LeftThumb = 1 << 5,     // 00001 00000

    RightThumb = 1 << 4,    // 00000 10000
    RightIndex = 1 << 3,    // 00000 01000
    RightMiddle = 1 << 2,   // 00000 00100
    RightRing = 1 << 1,     // 00000 00010
    RightPinky = 1 << 0     // 00000 00001
}
