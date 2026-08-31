using XyloType.Application;

namespace XyloType.Application.Interfaces;

public interface IChoosePath
{
    Task<Result<string?>> SelectPathAsync();
}
