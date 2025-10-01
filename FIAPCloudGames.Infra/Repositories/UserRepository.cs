using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Repositories;
using FIAPCloudGames.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FIAPCloudGames.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CloudGamesDbContext _context;
        public UserRepository(CloudGamesDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? GetById(Guid id)
        {
            return _context.Users.Include(u => u.Email).Include(u => u.Password).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.Email).Include(u => u.Password).ToList();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.Include(u => u.Email).Include(u => u.Password).FirstOrDefault(u => u.Email.Value == email);
        }
    }
}
