using FIAPCloudGames.Domain.Enums;
using FIAPCloudGames.Domain.ValueObject;

namespace FIAPCloudGames.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public UserRole Role { get; private set; }

        public User(string name, string email, string password, UserRole role = UserRole.User)
        {
            Id = Guid.NewGuid();
            UpdateName(name);
            UpdateEmail(email);
            UpdatePassword(password);
            Role = role;
        }


        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome é obrigatório.");
            Name = name;
        }

        public void UpdateEmail(string email)
        {
            Email = new Email(email);
        }

        public void UpdatePassword(string password)
        {
            Password = new Password(password);
        }
    }
}
