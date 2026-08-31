namespace XyloType.Application.Interfaces;

public interface IAssetReader
{
    Task<Stream> OpenAsync(string path);
}
