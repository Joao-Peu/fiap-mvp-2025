using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPCloudGames.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string email, string password);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<UserReadDto>> GetAllAsync();
        Task<UserReadDto?> GetByIdAsync(Guid id);
        Task<UserReadDto> RegisterAsync(string name, string email, string password);
        Task<UserReadDto?> UpdateAsync(Guid id, string name, string email, string password);
    }
}
