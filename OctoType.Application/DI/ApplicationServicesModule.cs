using Microsoft.Extensions.DependencyInjection;

using OctoType.Application.Interfaces;
using OctoType.Application.Services;

namespace OctoType.Application.DI;

internal static class ApplicationServicesModule
{
    public static IServiceCollection AddOctoTypeApplicationServices(this IServiceCollection services)
    {
        // ********************************************************************************************
        // Zero dependancies services
        // ********************************************************************************************
        services.AddTransient<IInputCharMapperService, InputCharMapperService>();
        services.AddTransient<IKeyboardAnalyzerService, KeyboardAnalyzerService>();
        services.AddTransient<IKeyBoardLayoutAvailableService, KeyBoardLayoutAvailableService>();
        services.AddTransient<ILanguageAvailableService, LanguageAvailableService>();
        services.AddTransient<IPseudoWordGeneratorService, PseudoWordGeneratorService>();
        services.AddTransient<IStringsProvider, DevStringsProvider>();
        services.AddTransient<IGenerationTypeSourceAvailableService, GenerationTypeSourceAvailableService>();
        services.AddTransient<ITypingExerciseWordNumberService, TypingExerciseWordNumberService>();
        services.AddTransient<ITypingExerciseLineNumberService, TypingExerciseLineNumberService>();
        services.AddTransient<IEditorSplitCharProvider, EditorSplitCharProvider>();
        services.AddTransient<IGuidProvider, GuidProvider>();
        services.AddTransient<IChartResponseTimeColorsProvider, ChartResponseTimeColorsProvider>();
        services.AddTransient<IChartErrorProvider, ChartErrorProvider>();

        // TODO
        // need to add qwerty etc keyboard keys locators
        services.AddTransient<IKeyboardKeysLocator, AzertyKeysLocator>();
        // ********************************************************************************************

        services.AddTransient<IPseudoWordBatchGenerator, PseudoWordBatchGenerator>();   // depends -> IPseudoWordGeneratorService


        return services;
    }
}
