using Microsoft.Extensions.DependencyInjection;

namespace OctoType.Application.DI;

static public class ApplicationModule
{
    public static IServiceCollection AddOctoTypeApplication(this IServiceCollection services)
    {
        services.AddOctoTypeApplicationServices();
        services.AddOctoTypeApplicationOrchestrators();
        return services;
    }
}
