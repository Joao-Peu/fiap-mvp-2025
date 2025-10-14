using FIAPCloudGames.Application.Interfaces;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;

namespace FIAPCloudGames.Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public User Register(string name, string email, string password)
        {
            var user = new User(name, email, password);
            _userRepository.Add(user);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User? GetById(Guid id)
        {
            return _userRepository.GetById(id);
        }

        public User? Update(Guid id, string name, string email, string password)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return null;
            }

            user.UpdateName(name);
            user.UpdateEmail(email);
            user.UpdatePassword(password);
            _userRepository.Update(user);
            return user;
        }

        public bool Delete(Guid id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return false;
            }

            _userRepository.Remove(user);
            return true;
        }

        public User? Authenticate(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user != null && user.Password.Value == password)
            {
                return user;
            }

            return null;
        }
    }
}
