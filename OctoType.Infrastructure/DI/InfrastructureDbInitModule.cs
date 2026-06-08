using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using OctoType.Infrastructure.DbContexts;

namespace OctoType.Infrastructure.DI;

public static class InfrastructureDbInitModule
{
    public static void InitUpgradeInfrastructure(this IServiceProvider services)
    {
        using IServiceScope scope =
            services.CreateScope();

        DactyloDbContext dbContext =
            scope.ServiceProvider
                .GetRequiredService<DactyloDbContext>();

        dbContext.Database.Migrate();
    }
}