using OctoType.Application;
using OctoType.Application.Interfaces;

namespace OctoType.Infrastructure.Providers;

public class FileCopyProvider : IFileCopyProvider
{
    public async Task<Result<bool>> CopyFileToAsync(string src, string dst, bool force)
    {
        if (!File.Exists(src))
        {
            return Result<bool>
                .Fail($"Src file {src} doesn't exists");
        }

        if (File.Exists(dst) && !force)
        {
            return Result<bool>
                .Fail($"Dst file {dst} already exists");
        }

        try
        {
            string? dir = Path.GetDirectoryName(dst);
            if(dir is null)
            {
                return Result<bool>
                .Fail($"Error on copy src : {src}, Dst file {dst}, error : directry path is null");
            }

            Directory.CreateDirectory(dir);
            File.Copy(src, dst, force);
        }
        catch (Exception ex)
        {
            return Result<bool>
                .Fail($"Error on move src : {src}, Dst file {dst}, error : {ex.Message}");
        }

        return Result<bool>
            .Ok(true);
    }
}
