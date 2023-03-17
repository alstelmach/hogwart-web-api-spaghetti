using Hogwart.Api.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hogwart.Api.Persistence;

public class SortingContext : DbContext
{
    private readonly PersistenceOptions _persistenceOptions;

    public SortingContext(
        DbContextOptions<SortingContext> options,
        IOptions<PersistenceOptions> persistenceOptions)
            : base(options) =>
                _persistenceOptions = persistenceOptions.Value;

    public virtual DbSet<HouseDto> Houses { get; init; }
    
    public virtual DbSet<StudentDto> Students { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        _persistenceOptions.ConfigureDbContext(optionsBuilder);
}