using XyloType.Application.ValueObjects;

namespace XyloType.Application.Interfaces;

public interface IPseudoWordGeneratorService
{
    Result<string> Generate(PseudoWordOptions options);
}
