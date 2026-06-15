namespace OctoType.Application.Interfaces
{
    public interface IFileCopyProvider
    {
        Task<Result<bool>> CopyFileToAsync(string src, string dst, bool force);
    }
}