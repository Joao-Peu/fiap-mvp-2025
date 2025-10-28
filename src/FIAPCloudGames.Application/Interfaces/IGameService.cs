using FIAPCloudGames.Application.Dtos;
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
        Task<IEnumerable<GameReadDto>> GetAllAsync();
        Task<GameReadDto?> GetByIdAsync(Guid id);
        Task<GameReadDto> RegisterAsync(string title, string description, DateTime releaseDate, decimal price);
        Task<GameReadDto?> UpdateAsync(Guid id, string? title, string? description, DateTime? releaseDate, decimal? price);
    }
}
