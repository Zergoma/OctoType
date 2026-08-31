using XyloType.Application.Interfaces;

namespace XyloType.Application.Services;

public class GuidProvider : IGuidProvider
{
    public Guid CreateGuid()
    {
        return Guid.NewGuid();
    }
}
