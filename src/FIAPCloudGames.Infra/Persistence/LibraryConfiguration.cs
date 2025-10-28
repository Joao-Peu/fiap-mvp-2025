using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Infra.Persistence.Configurations;

public class LibraryConfiguration : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("Libraries");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.IsDeleted).HasDefaultValue(false);

        builder.HasOne(l => l.User)
               .WithMany()
               .HasForeignKey("UserId")
               .IsRequired();

        // Relacionamento com LibraryGame
        builder.HasMany(l => l.OwnedGames)
               .WithOne(g => g.Library)
               .HasForeignKey("LibraryId");
    }
}