using XyloType.Application.Interfaces;

namespace XyloType.Infrastructure;

public class MauiAssetReader : IAssetReader
{
    public Task<Stream> OpenAsync(string path)
    {
        return FileSystem.OpenAppPackageFileAsync(path);
    }
}