using OctoType.Application.Interfaces;

namespace OctoType.Infrastructure;

public class MauiAssetReader : IAssetReader
{
    public Task<Stream> OpenAsync(string path)
    {
        return FileSystem.OpenAppPackageFileAsync(path);
    }
}