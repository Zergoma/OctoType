using OctoType.Application;
using OctoType.Application.Interfaces;

namespace OctoType.Infrastructure.Providers;

public class FileSaverProvider : IFileSaverProvider
{
    public async Task<Result<bool>> SaveToAsync(string src, string dst, bool force)
    {
        if (File.Exists(src) is false)
        {
            return Result<bool>
                .Fail($"Src file {src} doesn't exists");
        }

        if (File.Exists(dst) && force is false)
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
