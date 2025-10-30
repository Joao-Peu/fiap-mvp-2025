using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Infra.Persistence;

public class LibraryGameConfiguration : IEntityTypeConfiguration<LibraryGame>
{
    public void Configure(EntityTypeBuilder<LibraryGame> builder)
    {
        builder.ToTable("LibraryGames");
        builder.HasKey(lg => lg.Id);

        builder.Property(lg => lg.AcquiredAt)
               .IsRequired();

        builder.HasOne(lg => lg.Library)
               .WithMany(l => l.OwnedGames)
               .HasForeignKey("LibraryId")
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(lg => lg.Game)
               .WithMany()
               .HasForeignKey("GameId")
               .IsRequired();

        builder.HasIndex("LibraryId", "GameId")
               .IsUnique();
    }
}