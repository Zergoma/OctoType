using OctoType.Application.Interfaces;

namespace OctoType.Application.Services;

public class GuidProvider : IGuidProvider
{
    public Guid CreateGuid()
    {
        return Guid.NewGuid();
    }
}
