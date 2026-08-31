using XyloType.Domain.Entities;

namespace XyloType.Application.Models;

public class UnitTextAnalysis
{
    public bool UsesLeftHand { get; set; }

    public bool UsesRightHand { get; set; }

    public KeyboardRow RowMask { get; set; }

    public Finger FingerMask { get; set; }

    public bool ExternalAccent { get; set; }
}
