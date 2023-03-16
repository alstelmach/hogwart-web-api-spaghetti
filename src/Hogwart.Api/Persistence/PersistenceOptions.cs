using Microsoft.EntityFrameworkCore;

namespace Hogwart.Api.Persistence;

public sealed class PersistenceOptions
{
    public string ConnectionString { get; init; }

    public bool IsDebugMode { get; init; }

    public bool MigrateDatabaseOnStartup { get; init; }

    public void ConfigureDbContext(DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        dbContextOptionsBuilder.UseNpgsql(ConnectionString);

        if (IsDebugMode)
        {
            dbContextOptionsBuilder
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }
    }
}
