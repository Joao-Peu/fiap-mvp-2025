using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;
using FIAPCloudGames.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Infra.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly CloudGamesDbContext _context;
        public GameRepository(CloudGamesDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task<Game?> GetByIdAsync(Guid id)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }
}
