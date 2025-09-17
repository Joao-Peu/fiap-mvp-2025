using System;
using System.Collections.Generic;

namespace FIAPCloudGames.Domain.Repositories
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
