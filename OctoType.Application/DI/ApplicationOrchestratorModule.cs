using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Application.Orchestrators;

namespace OctoType.Application.DI;

static internal class ApplicationOrchestratorModule
{
    public static IServiceCollection AddOctoTypeApplicationOrchestrators(this IServiceCollection services)
    {
        services.AddTransient<IWordImportServiceOrchestrator, WordImportOrchestrator>();

        return services;
    }

}
