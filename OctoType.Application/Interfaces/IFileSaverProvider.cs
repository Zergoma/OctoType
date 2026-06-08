using OctoType.Application;

namespace OctoType.Application.Interfaces
{
    public interface IFileSaverProvider
    {
        Task<Result<bool>> SaveToAsync(string src, string dst, bool force);
    }
}