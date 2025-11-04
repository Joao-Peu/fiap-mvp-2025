using FIAPCloudGames.Domain.Enums;
using FIAPCloudGames.Domain.ValueObject;

namespace FIAPCloudGames.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }

    private User()
    {

    }

    public User(string name, string email, string password, UserRole role = UserRole.User)
    {
        Id = Guid.NewGuid();
        IsActive = true;
        UpdateName(name);
        UpdateEmail(email);
        UpdatePassword(password);
        Role = role;
    }


    public void UpdateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
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

    public void Inactivate() => IsActive = false;
}
