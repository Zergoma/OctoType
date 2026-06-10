using OctoType.Application.ValueObjects;

namespace OctoType.Application.Interfaces;

public interface IPseudoWordBatchGenerator
{
    Result<List<string>> Generate(int count, PseudoWordOptions options);
}
