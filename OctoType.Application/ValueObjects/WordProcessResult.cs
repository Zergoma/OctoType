using OctoType.Domain.Entities;

namespace OctoType.Application.ValueObjects;

public record WordProcessResult(Word[] NewWords, Word[] UpdatedWords, String[] NoMapWords);
