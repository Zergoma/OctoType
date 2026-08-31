using FluentValidation;

using OctoType.Application.ValueObjects;

namespace OctoType.Application.Validators;

public class FilteredCharTypesValidator : AbstractValidator<FilteredCharTypes>
{
    public FilteredCharTypesValidator()
    {
        RuleFor(p => p)
            .Must(p => !string.IsNullOrEmpty(p.Vowels) || !string.IsNullOrEmpty(p.Consonants))
            .WithMessage("Vowels and consonant are null/empty");
            
    }
}


