using Microsoft.Extensions.DependencyInjection;

namespace XyloType.Application.DI;

static public class ApplicationModule
{
    public static IServiceCollection AddOctoTypeApplication(this IServiceCollection services)
    {
        services.AddOctoTypeApplicationFactories();
        services.AddOctoTypeApplicationValidators();
        services.AddOctoTypeApplicationServices();
        services.AddOctoTypeApplicationManagers();
        services.AddOctoTypeApplicationOrchestrators();
        services.AddOctoTypeApplicationUseCases();
        
        return services;
    }
}
