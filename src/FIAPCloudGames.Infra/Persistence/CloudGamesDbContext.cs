using Microsoft.EntityFrameworkCore;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.ValueObject;

namespace FIAPCloudGames.Infra.Persistence
{
    public class CloudGamesDbContext : DbContext
    {
        public CloudGamesDbContext(DbContextOptions<CloudGamesDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<LibraryGame> LibraryGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.IsActive);
            modelBuilder.Entity<Library>().HasQueryFilter(u => u.IsActive);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CloudGamesDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
