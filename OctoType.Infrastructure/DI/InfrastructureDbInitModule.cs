using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using OctoType.Infrastructure.DbContexts;

using System.Diagnostics;

namespace OctoType.Infrastructure.DI;

public static class InfrastructureDbInitModule
{
    public static void InitUpgradeInfrastructure(this IServiceProvider services)
    {
        using IServiceScope scope =
            services.CreateScope();
        try
        {
            DactyloDbContext dbContext =
                scope.ServiceProvider
                    .GetRequiredService<DactyloDbContext>();

            dbContext.Database.Migrate();

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}