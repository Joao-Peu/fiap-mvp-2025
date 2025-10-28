using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Infra.Persistence;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Title).IsRequired().HasMaxLength(200);
        builder.Property(g => g.Description).HasMaxLength(1000);
        builder.Property(g => g.ReleaseDate).IsRequired();
        builder.Property(g => g.Price).HasColumnType("decimal(18,2)").IsRequired();
    }
}