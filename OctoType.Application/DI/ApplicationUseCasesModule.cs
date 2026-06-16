using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Application.UseCases;

namespace OctoType.Application.DI;

static internal class ApplicationUseCasesModule
{
    public static IServiceCollection AddOctoTypeApplicationUseCases(this IServiceCollection services)
    {
        services.AddTransient<ISaveTypingExerciceUseCase, SaveTypingExerciceUseCase>();

        return services;
    }

}
