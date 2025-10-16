using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Domain.Interfaces;

public interface ILibraryRepository
{
    Task AddAsync(Library library);
    Task<IEnumerable<Library>> GetAllAsync();
    Task<Library?> GetByIdAsync(Guid id);
    Task<Library?> GetByUserIdAsync(Guid userId);
    Task RemoveAsync(Library library);
    Task UpdateAsync(Library library);
    Task<bool> ContainsGameAsync(Guid libraryId, Guid gameId);

}
