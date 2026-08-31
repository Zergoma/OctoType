namespace XyloType.Infrastructure.DbContexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DactyloDbContextDesignTimeFactory : IDesignTimeDbContextFactory<DactyloDbContext>
{
    public DactyloDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<DactyloDbContext>();

        var dbPath =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "DactyloDesignTime.db3"
                );

        optionBuilder.UseSqlite($"Data Source={dbPath}");

        return new DactyloDbContext(optionBuilder.Options);
    }
}
