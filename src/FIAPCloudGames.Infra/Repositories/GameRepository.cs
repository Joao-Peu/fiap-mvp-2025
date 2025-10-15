using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;
using FIAPCloudGames.Infra.Persistence;

namespace FIAPCloudGames.Infra.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly CloudGamesDbContext _context;
        public GameRepository(CloudGamesDbContext context)
        {
            _context = context;
        }

        public void Add(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public Game? GetById(Guid id)
        {
            return _context.Games.FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games.ToList();
        }

        public void Update(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }

        public void Remove(Game game)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }
    }
}
