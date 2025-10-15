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
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> RegisterAsync(string name, string email, string password);
        Task<User?> UpdateAsync(Guid id, string name, string email, string password);
    }
}
