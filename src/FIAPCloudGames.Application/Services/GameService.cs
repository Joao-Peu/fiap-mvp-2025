using FIAPCloudGames.Application.Interfaces;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;

namespace FIAPCloudGames.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Game Register(string title, string description, DateTime releaseDate, decimal price)
        {
            var game = new Game(title, description, releaseDate, price);
            _gameRepository.Add(game);
            return game;
        }

        public IEnumerable<Game> GetAll()
        {
            return _gameRepository.GetAll();
        }

        public Game? GetById(Guid id)
        {
            return _gameRepository.GetById(id);
        }

        public Game? Update(Guid id, string title, string description, DateTime releaseDate, decimal price)
        {
            var game = _gameRepository.GetById(id);
            if (game == null)
            {
                return null;
            }

            game.Update(title, description, releaseDate, price);
            _gameRepository.Update(game);
            return game;
        }

        public bool Delete(Guid id)
        {
            var game = _gameRepository.GetById(id);
            if (game == null)
            {
                return false;
            }

            _gameRepository.Remove(game);
            return true;
        }
    }
}
