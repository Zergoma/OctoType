using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Application.Services;

namespace OctoType.Application.DI;

static internal class ApplicationServicesModule
{
    public static IServiceCollection AddOctoTypeApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IStringsProviderService, StringsProviderService>();
        services.AddTransient<IInputCharMapperService, InputCharMapperService>();
        services.AddTransient<IWordBatchProcessorService, WordBatchProcessorService>();
        services.AddTransient<IKeyboardAnalyzerService, KeyboardAnalyzerService>();
        services.AddTransient<IPseudoWordGeneratorService, PseudoWordGeneratorService>();

        services.AddTransient<IKeyboardKeyLocator, AzertyKeyLocator>();
        services.AddTransient<IKeyboardKeyLocatorManager, KeyboardKeyLocatorManager>();

        return services;
    }
}
