using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Infra.Persistence;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.OwnsOne(typeof(Domain.ValueObject.Email), "Email", b =>
        {
            b.Property("Value").HasColumnName("Email").IsRequired().HasMaxLength(255);
        });

        builder.OwnsOne(typeof(Domain.ValueObject.Password), "Password", b =>
        {
            b.Property("Value").HasColumnName("Password").IsRequired().HasMaxLength(255);
        });

        builder.Property("Role")
               .HasConversion<string>()
               .HasColumnName("Role")
               .IsRequired();
    }
}