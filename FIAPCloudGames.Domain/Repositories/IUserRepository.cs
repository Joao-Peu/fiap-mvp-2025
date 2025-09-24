using System;
using System.Collections.Generic;

namespace FIAPCloudGames.Domain.Repositories
{
    public interface IUserRepository
    {
        void Add(Entities.User user);
        Entities.User? GetById(Guid id);
        IEnumerable<Entities.User> GetAll();
        void Update(Entities.User user);
        void Remove(Entities.User user);
        Entities.User? GetByEmail(string email);
    }
}
