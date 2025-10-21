using Microsoft.EntityFrameworkCore;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.ValueObject;

namespace FIAPCloudGames.Infra.Persistence;

public class CloudGamesDbContext(DbContextOptions<CloudGamesDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Library> Libraries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasQueryFilter(u => u.IsActive);
        modelBuilder.Entity<Library>().HasQueryFilter(u => u.IsActive);
    }
}
