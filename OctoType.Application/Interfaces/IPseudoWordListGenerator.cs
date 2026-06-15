using OctoType.Application.ValueObjects;

namespace OctoType.Application.Interfaces;

public interface IPseudoWordListGenerator
{
    Result<List<string>> Generate(int count, PseudoWordOptions options);
}
