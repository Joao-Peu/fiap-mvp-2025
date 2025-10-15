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
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(Guid id);
        Task<Game> RegisterAsync(string title, string description, DateTime releaseDate, decimal price);
        Task<Game?> UpdateAsync(Guid id, string? title, string? description, DateTime? releaseDate, decimal? price);
    }
}
