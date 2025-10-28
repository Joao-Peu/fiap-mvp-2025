using FIAPCloudGames.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.API.Extensions;

public static class DatabaseConfigurationExtensions
{
    public static IServiceCollection AddDatabaseConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            services.AddDbContext<CloudGamesDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
        else
        {
            services.AddDbContext<CloudGamesDbContext>(options =>
                options.UseInMemoryDatabase("CloudGamesDB"));
        }

        return services;
    }
}
