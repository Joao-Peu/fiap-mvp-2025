using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;
using FIAPCloudGames.Infra.Persistence;

namespace FIAPCloudGames.Infra.Repositories
{
    public class LibraryRepository(CloudGamesDbContext context) : ILibraryRepository
    {
        private readonly CloudGamesDbContext _context = context;

        public void Add(Library library)
        {
            _context.Libraries.Add(library);
            _context.SaveChanges();
        }

        public Library? GetById(Guid id)
        {
            return _context.Libraries.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Library> GetAll()
        {
            return [.. _context.Libraries];
        }

        public void Update(Library library)
        {
            _context.Libraries.Update(library);
            _context.SaveChanges();
        }

        public void Remove(Library library)
        {
            _context.Libraries.Remove(library);
            _context.SaveChanges();
        }

        public Library? GetByUserId(Guid userId)
        {
            return _context.Libraries.FirstOrDefault(u => u.User.Id == userId);
        }
    }
}
