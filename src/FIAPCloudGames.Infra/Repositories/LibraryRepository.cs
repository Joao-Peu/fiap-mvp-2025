using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;
using FIAPCloudGames.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Infra.Repositories
{
    public class LibraryRepository(CloudGamesDbContext context) : ILibraryRepository
    {
        private readonly CloudGamesDbContext _context = context;

        public async Task AddAsync(Library library)
        {
            await _context.Libraries.AddAsync(library);
            await _context.SaveChangesAsync();
        }

        public async Task<Library?> GetByIdAsync(Guid id)
        {
            return await _context.Libraries
                .Include(l => l.User)  // Incluir User
                .Include(l => l.OwnedGames)
                    .ThenInclude(og => og.Game)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            return await _context.Libraries
                .Include(l => l.User)  // Incluir User
                .Include(l => l.OwnedGames)
                    .ThenInclude(og => og.Game)
                .ToListAsync();
        }

        public async Task UpdateAsync(Library library)
        {
            _context.Libraries.Update(library);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Library library)
        {
            _context.Libraries.Remove(library);
            await _context.SaveChangesAsync();
        }

        public async Task<Library?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Libraries
                .Include(l => l.User)  // Incluir User
                .Include(l => l.OwnedGames)
                    .ThenInclude(og => og.Game)
                .FirstOrDefaultAsync(u => u.User.Id == userId);
        }

        public async Task<bool> ContainsGameAsync(Guid libraryId, Guid gameId)
        {
           return await _context.Set<LibraryGame>()
               .AnyAsync(x => x.Game.Id == gameId && x.Library.Id == libraryId);
        }

        public async Task AddGameToLibraryAsync(Guid libraryId, Guid gameId)
        {
            // Buscar as entidades necessárias
            var library = await _context.Libraries.FindAsync(libraryId);
            var game = await _context.Games.FindAsync(gameId);
            
            if (library == null || game == null)
                throw new InvalidOperationException("Library or Game not found");

            var libraryGame = new LibraryGame(library, game);
            await _context.LibraryGames.AddAsync(libraryGame);
            await _context.SaveChangesAsync();
        }
    }
}
