using CarNotesTelegramBot.Constants;
using CarNotesTelegramBot.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarNotesTelegramBot.DbContexts;

/// <summary>
/// The app db context class.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<FeaturesConfig> FeaturesConfigs { get; set; } = null!;
    public DbSet<CarFeatureEntity> CarFeatures { get; set; } = null!;
    public DbSet<CarDataEntity> CarData { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FeaturesConfig>().ToTable(DomainConstants.FeaturesConfigTableName);
        modelBuilder.Entity<CarFeatureEntity>().ToTable(DomainConstants.CarFeaturesTableName);
        modelBuilder.Entity<CarDataEntity>().ToTable(DomainConstants.CarDataTableName);

        modelBuilder.Entity<CarFeatureEntity>()
            .HasOne(x => x.Feature)
            .WithMany()
            .HasForeignKey(x => x.FeatureId);
    }
}