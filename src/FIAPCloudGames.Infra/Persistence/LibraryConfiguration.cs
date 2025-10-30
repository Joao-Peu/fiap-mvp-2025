using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Infra.Persistence;

public class LibraryConfiguration : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("Libraries");
        builder.HasKey(l => l.Id);

        // Configurar propriedade IsActive
        builder.Property(l => l.IsActive)
               .IsRequired()
               .HasDefaultValue(true);

        // Relacionamento com User (um usuário tem uma biblioteca)
        builder.HasOne(l => l.User)
               .WithMany()
               .HasForeignKey("UserId")
               .IsRequired();

        // Relacionamento com LibraryGame
        builder.HasMany(l => l.OwnedGames)
               .WithOne(g => g.Library)
               .HasForeignKey("LibraryId")
               .OnDelete(DeleteBehavior.Cascade);

        // Índice único para garantir que um usuário tenha apenas uma biblioteca
        builder.HasIndex("UserId")
               .IsUnique();
    }
}