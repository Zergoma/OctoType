using FluentValidation;

using OctoType.Application.UseCases;

namespace OctoType.Application.Validators;

public class TypingExerciseCreateParametersValidator : AbstractValidator<TypingExerciseCreateParameters>
{
    public TypingExerciseCreateParametersValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is empty");

        RuleFor(x => x.AllowedLetters)
            .NotEmpty()
            .WithMessage("No letters selected");

        RuleFor(x => x.KeyBoardLayoutDto)
            .NotNull()
            .WithMessage("You need to select a keybord");
    }
}

