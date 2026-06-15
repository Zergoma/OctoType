using Microsoft.Extensions.DependencyInjection;

namespace OctoType.Application.DI;

static public class ApplicationModule
{
    public static IServiceCollection AddOctoTypeApplication(this IServiceCollection services)
    {
        services.AddOctoTypeApplicationFactories();
        services.AddOctoTypeApplicationServices();
        services.AddOctoTypeApplicationManager();
        services.AddOctoTypeApplicationOrchestrators();
        return services;
    }
}
