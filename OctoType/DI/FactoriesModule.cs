using OctoType.Factories;
using OctoType.Infrastructure.DbContexts;
using OctoType.Interfaces;

namespace OctoType.DI;

public static class FactoriesModule
{
    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddTransient<ITypingCharFactory, TypingCharFactory>();
        services.AddTransient<ITypingLineStateFactory, TypingLineStateFactory>();
        return services;
    }
}
