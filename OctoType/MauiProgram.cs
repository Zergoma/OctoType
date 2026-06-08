using CommunityToolkit.Maui;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;

using OctoType.Application.DI;
using OctoType.DI;
using OctoType.Infrastructure.DbContexts;
using OctoType.Infrastructure.DI;


namespace OctoType;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
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
