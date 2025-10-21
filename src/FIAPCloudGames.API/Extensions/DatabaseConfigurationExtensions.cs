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

        services.AddDbContext<CloudGamesDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
