using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Infra.Seeders;

public class UserSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var adminId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        modelBuilder.Entity<User>().HasData(new
        {
            Id = adminId,
            Name = "Administrador",
            Role = UserRole.Admin,
            IsActive = true
        });

        modelBuilder.Entity<User>()
            .OwnsOne(u => u.Email)
            .HasData(new { UserId = adminId, Value = "admin@seudominio.com" });

        modelBuilder.Entity<User>()
            .OwnsOne(u => u.Password)
            .HasData(new { UserId = adminId, Value = "SenhaAdmin@123" });
    }
}
