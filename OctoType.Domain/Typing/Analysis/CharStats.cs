namespace OctoType.Domain.Typing.Analysis;

public class CharStats
{
    public int NbOccurence { get; set; } = 1;

    // juste 1 for 1 char, don't count how many attempt to success for it
    public int NbCharError { get; set; } = 0;

    public List<char> RealErrors { get; set; } = [];

    public TimeSpan RespondeTime { get; set; } = TimeSpan.Zero;

    public TimeSpan ResponseTimeAverage
    { 
        get
        {
            int occ = Math.Max(NbOccurence, 1);
            return RespondeTime / occ;
        }
    }

    public CharStats Add(CharStats B) =>
        new()
        {
            NbOccurence = NbOccurence + B.NbOccurence,
            NbCharError = NbCharError + B.NbCharError,
            RealErrors = [.. RealErrors, .. B.RealErrors],
            RespondeTime = RespondeTime + B.RespondeTime
        };
}