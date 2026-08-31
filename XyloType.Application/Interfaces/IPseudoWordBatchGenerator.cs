using XyloType.Application;
using XyloType.Application.ValueObjects;

namespace XyloType.Application.Interfaces;

public interface IPseudoWordBatchGenerator
{
    Result<List<string>> Generate(int count, PseudoWordOptions options);
}
