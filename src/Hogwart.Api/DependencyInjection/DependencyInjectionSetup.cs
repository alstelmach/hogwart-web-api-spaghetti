using Hogwart.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hogwart.Api.DependencyInjection;

public static class DependencyInjectionSetup
{
    public static WebApplicationBuilder ConfigureDependencyInjection(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder
            .Services
            .AddControllers()
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .Configure<PersistenceOptions>(webApplicationBuilder.Configuration.GetSection(nameof(PersistenceOptions)))
            .AddDbContext<SortingContext>();

        var isDbMigrationRequired = webApplicationBuilder
            .Configuration
            .GetValue<bool>($"{nameof(PersistenceOptions)}:MigrateDatabaseOnStartup");

        if (isDbMigrationRequired)
        {
            var dbContext = webApplicationBuilder
                .Services
                .BuildServiceProvider()
                .GetRequiredService<SortingContext>();
            
            dbContext.Database.Migrate();
        }

        return webApplicationBuilder;
    }
}
