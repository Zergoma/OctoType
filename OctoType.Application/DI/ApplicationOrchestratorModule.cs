using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Application.Orchestrators;

namespace OctoType.Application.DI;

static internal class ApplicationOrchestratorModule
{
    public static IServiceCollection AddOctoTypeApplicationOrchestrators(this IServiceCollection services)
    {
        // ****************************************************************************************************
        // Order manner
        // ****************************************************************************************************
        services.AddTransient<IWordBatchProcessorOrchestrator, WordBatchProcessorOrchestrator>();   // depends ->  IKeyboardAnalyzerService
        services.AddTransient<IWordImportOrchestrator, WordImportOrchestrator>();                   // depends ->  IWordBatchProcessorOrchestrator
        // ****************************************************************************************************

        return services;
    }
}
