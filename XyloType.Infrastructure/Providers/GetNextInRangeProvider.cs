using XyloType.Application.Interfaces;

namespace XyloType.Infrastructure.Providers;

public class GetNextInRangeProvider : IGetNextInRange
{
    private static Random _random = Random.Shared;
    public int GetNext(int min, int max)
    {
        return _random.Next(min, max);
    }
}
