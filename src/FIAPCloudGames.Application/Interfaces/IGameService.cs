using FIAPCloudGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Application.Interfaces
{
    public interface IGameService
    {
        bool Delete(Guid id);
        IEnumerable<Game> GetAll();
        Game? GetById(Guid id);
        Game Register(string title, string description, DateTime releaseDate, decimal price);
        Game? Update(Guid id, string title, string description, DateTime releaseDate, decimal price);
    }
}
