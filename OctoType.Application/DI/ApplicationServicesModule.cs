using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Application.Orchestrators;
using OctoType.Application.Services;

namespace OctoType.Application.DI;

static internal class ApplicationServicesModule
{
    public static IServiceCollection AddOctoTypeApplicationServices(this IServiceCollection services)
    {
        // ********************************************************************************************
        // Zero dependancies services
        // ********************************************************************************************
        services.AddTransient<IKeyboardKeysLocator, AzertyKeysLocator>();
        services.AddTransient<IInputCharMapperService, InputCharMapperService>();
        services.AddTransient<IKeyboardAnalyzerService, KeyboardAnalyzerService>();
        services.AddTransient<IKeyBoardLayoutAvailableService, KeyBoardLayoutAvailableService>();
        services.AddTransient<ILanguageAvailableService, LanguageAvailableService>();
        services.AddTransient<IPseudoWordGeneratorService, PseudoWordGeneratorService>();
        services.AddTransient<IStringsProviderService, StringsProviderService>();
        // ********************************************************************************************

        return services;
    }

    public static IServiceCollection AddOctoTypeApplicationManager(this IServiceCollection services)
    {
        services.AddTransient<IKeyboardKeyLocatorManager, KeyboardKeyLocatorManager>(); // depends -> IKeyboardKeysLocator

        return services;
    }
}

