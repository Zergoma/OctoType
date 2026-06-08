using OctoType.Application;

namespace OctoType.Application.Interfaces;

public interface IChoosePath
{
    Task<Result<string?>> SelectPathAsync();
}
