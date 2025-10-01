namespace FIAPCloudGames.Domain.Interfaces
{
    public interface IGameRepository
    {
        void Add(Entities.Game game);
        Entities.Game? GetById(Guid id);
        IEnumerable<Entities.Game> GetAll();
        void Update(Entities.Game game);
        void Remove(Entities.Game game);
    }
}
