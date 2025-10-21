using FIAPCloudGames.Application.Interfaces;
using FIAPCloudGames.Application.Services;
using FIAPCloudGames.Domain.Interfaces;
using FIAPCloudGames.Infra.Repositories;

namespace FIAPCloudGames.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<ILibraryRepository, LibraryRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<ILibraryService, LibraryService>();

        return services;
    }
}