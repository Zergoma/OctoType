using XyloType.Domain.Entities;

namespace XyloType.Application.ValueObjects;

public record WordProcessResult(Word[] NewWords, Word[] UpdatedWords, String[] NoMapWords);
