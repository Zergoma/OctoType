using OctoType.Application.ValueObjects;

namespace OctoType.Application.Interfaces;

public interface IPseudoWordGeneratorService
{
    Result<string> Generate(PseudoWordOptions options);
}
