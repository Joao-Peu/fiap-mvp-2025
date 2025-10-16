namespace FIAPCloudGames.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(Entities.User user);
    Task<Entities.User?> GetByIdAsync(Guid id);
    Task<IEnumerable<Entities.User>> GetAllAsync();
    Task UpdateAsync(Entities.User user);
    Task RemoveAsync(Entities.User user);
    Task<Entities.User?> GetByEmailAsync(string email);
}
