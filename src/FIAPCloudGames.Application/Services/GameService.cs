using FIAPCloudGames.Application.Adapters;
using FIAPCloudGames.Application.Dtos;
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

    public async Task<GameReadDto> RegisterAsync(string title, string description, DateTime releaseDate, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
        if (price < 0)
        {
            throw new GameNegativePriceException();
        }

        var game = Game.New(Guid.NewGuid(), title, description, releaseDate, price);
        await _gameRepository.AddAsync(game);
        return game.ToDto();
    }

    public async Task<IEnumerable<GameReadDto>> GetAllAsync()
    {
        var games = await _gameRepository.GetAllAsync();
        return games.ToDto();
    }

    public async Task<GameReadDto?> GetByIdAsync(Guid id)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        return game?.ToDto();
    }

    public async Task<GameReadDto?> UpdateAsync(Guid id, string? title, string? description, DateTime? releaseDate, decimal? price)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        if (game == null)
        {
            return null;
        }

        game.Update(title, description, releaseDate, price);
        await _gameRepository.UpdateAsync(game);
        return game.ToDto();
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
