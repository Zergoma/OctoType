using FluentValidation;

using XyloType.Application.ValueObjects;

namespace XyloType.Application.Validators;

public class FilteredCharTypesValidator : AbstractValidator<FilteredCharTypes>
{
    public FilteredCharTypesValidator()
    {
        RuleFor(p => p)
            .Must(p => !string.IsNullOrEmpty(p.Vowels) || !string.IsNullOrEmpty(p.Consonants))
            .WithMessage("Vowels and consonant are null/empty");
            
    }
}


