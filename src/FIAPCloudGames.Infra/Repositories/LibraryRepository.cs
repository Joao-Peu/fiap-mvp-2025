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
            return await _context.Libraries.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            return await _context.Libraries.ToListAsync();
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
            return await _context.Libraries.FirstOrDefaultAsync(u => u.User.Id == userId);
        }

        public async Task<bool> ContainsGameAsync(Guid libraryId, Guid gameId)
        {
           return (await _context.Set<LibraryGame>().FirstOrDefaultAsync(x => x.Game.Id == gameId && x.Library.Id == libraryId)) != null;
        }
    }
}
