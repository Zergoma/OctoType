using XyloType.Domain.Enums;

namespace XyloType.Domain.Entities;

public sealed class WordAnalysis
{
    public int Id { get; set; }

    public KeyboardLayout Layout { get; set; }

    public bool UsesLeftHand { get; set; }

    public bool UsesRightHand { get; set; }

    public KeyboardRow RowMask { get; set; }

    public Finger FingerMask { get; set; }

    public bool ExternalAccent { get; set; }

    public int WordId { get; set; }
    public Word Word { get; set; } = null!;
}