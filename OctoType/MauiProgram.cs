using CommunityToolkit.Maui;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;

using OctoType.Application.DI;
using OctoType.DI;
using OctoType.Infrastructure.DbContexts;
using OctoType.Infrastructure.DI;

using Serilog;


namespace OctoType;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Log.Logger =
            new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    Path.Combine(FileSystem.AppDataDirectory, "logs/OctoType-.txt"),
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();


        var builder = MauiApp.CreateBuilder();

        builder.Logging
                .ClearProviders()
                .AddSerilog(Log.Logger);

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services
            .AddOctoTypeInfrastructure()

            .AddPresenters()
            .AddOctoTypeApplication()

            .AddFactories()
            .AddTypingThemes()
            .AddViewModels()
            .AddViews();
        
        string databasePath =
            Path.Combine(
                FileSystem.AppDataDirectory,
                "dactylo.db3");

        builder.Services.AddDbContextFactory<DactyloDbContext>(
            options => 
                options.UseSqlite($"Data Source={databasePath}"));

        var app = builder.Build();

        // Infrastructure operation : init or upgrade db according to migration state
        InfrastructureDbInitModule.InitUpgradeInfrastructure(app.Services);

        return app;
    }
}
