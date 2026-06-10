namespace OctoType.Application.ValueObjects;

public readonly record struct PseudoWordOptions(
    string AllowedChars,
    int MinLength,
    int MaxLength);
