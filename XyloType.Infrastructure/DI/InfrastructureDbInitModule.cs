using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

using XyloType.Infrastructure.DbContexts;

namespace XyloType.Infrastructure.DI;

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