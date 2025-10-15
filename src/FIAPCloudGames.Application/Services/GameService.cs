using FIAPCloudGames.Application.Interfaces;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Exceptions;
using FIAPCloudGames.Domain.Interfaces;

namespace FIAPCloudGames.Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Game> RegisterAsync(string title, string description, DateTime releaseDate, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
        if (price < 0)
        {
            throw new GameNegativePriceException();
        }

        var game = Game.New(Guid.NewGuid(), title, description, releaseDate, price);
        await _gameRepository.AddAsync(game);
        return game;
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _gameRepository.GetAllAsync();
    }

    public async Task<Game?> GetByIdAsync(Guid id)
    {
        return await _gameRepository.GetByIdAsync(id);
    }

    public async Task<Game?> UpdateAsync(Guid id, string? title, string? description, DateTime? releaseDate, decimal? price)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        if (game == null)
        {
            return null;
        }

        game.Update(title, description, releaseDate, price);
        await _gameRepository.UpdateAsync(game);
        return game;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        if (game == null)
        {
            return false;
        }

        await _gameRepository.RemoveAsync(game);
        return true;
    }
}
