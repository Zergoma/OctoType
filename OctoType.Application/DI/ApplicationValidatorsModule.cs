using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Models.Typing.Exercices;
using OctoType.Application.UseCases;
using OctoType.Application.Validators;

namespace OctoType.Application.DI;

internal static class ApplicationValidatorsModule
{
    public static IServiceCollection AddOctoTypeApplicationValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<TypingExerciseCreateParameters>, TypingExerciseCreateParametersValidator>();
        services.AddTransient<IValidator<TypingExercise>, TypingExerciceValidator>();
        return services;
    }
}