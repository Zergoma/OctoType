using OctoType.Domain.Entities;
using OctoType.Domain.Enums;

namespace OctoType.Domain.Models;

public sealed class WordQueryBuilder
{
    private WordSearchCriteria _criteria = new();

    public WordQueryBuilder WithLanguages(params string[] languages)
    {
        _criteria.LanguagesCodes = languages;
        return this;
    }

    public WordQueryBuilder WithMinLength(int value)
    {
        _criteria = _criteria with { MinLength = value };
        return this;
    }

    public WordQueryBuilder WithMaxLength(int value)
    {
        _criteria = _criteria with { MaxLength = value };
        return this;
    }

    public WordQueryBuilder WithFinger(Finger finger)
    {
        _criteria = _criteria with { FingerMask = finger };
        return this;
    }

    public WordQueryBuilder WithRow(KeyboardRow row)
    {
        _criteria = _criteria with { RowMask = row };
        return this;
    }

    public WordQueryBuilder WithLayout(KeyboardLayout layout)
    {
        _criteria = _criteria with { Layout = layout };
        return this;
    }

    public WordSearchCriteria Build() => _criteria;
}
