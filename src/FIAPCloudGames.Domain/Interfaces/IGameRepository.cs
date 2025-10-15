namespace FIAPCloudGames.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task AddAsync(Entities.Game game);
        Task<Entities.Game?> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Game>> GetAllAsync();
        Task UpdateAsync(Entities.Game game);
        Task RemoveAsync(Entities.Game game);
    }
}
