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
        User? Authenticate(string email, string password);
        bool Delete(Guid id);
        IEnumerable<User> GetAll();
        User? GetById(Guid id);
        User Register(string name, string email, string password);
        User? Update(Guid id, string name, string email, string password);
    }
}
