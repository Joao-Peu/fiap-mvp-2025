using Bogus;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Enums;
using FIAPCloudGames.Domain.Exceptions;

namespace FIAPCloudGames.Tests.Domain;

public class UserTests
{
    [Fact]
    public async Task New_ShoulReturnNewUserAtive()
    {
        var faker = new Faker();
        var name = faker.Person.FullName;
        var email = faker.Internet.Email();
        var password = "Valid@123";

        var user = new User(name, email, password);

        Assert.NotNull(user);
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email.Value);
        Assert.Equal(password, user.Password.Value);
        Assert.Equal(UserRole.User, user.Role);
        Assert.True(user.IsActive);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public async Task UpdateName_ShouldThrowException_WhenNameIsNullOrWhiteSpace(string name)
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        Assert.Throws<ArgumentException>(() => user.UpdateName(name));
    }

    [Fact]
    public async Task UpdateName_ShouldThrowException_WhenNameIsNull()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        Assert.Throws<ArgumentNullException>(() => user.UpdateName(null));
    }

    [Fact]
    public async Task UpdateName_ShouldUpdateName()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var newName = faker.Person.FullName;

        user.UpdateName(newName);

        Assert.Equal(newName, user.Name);
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("@example.com")]
    [InlineData("test@")]
    [InlineData("")]
    public async Task UpdateEmail_ShouldThrowException_WhenInvalid(string invalidEmail)
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        Assert.Throws<EmailInvalidException>(() => user.UpdateEmail(invalidEmail));
    }

    [Fact]
    public async Task UpdateEmail_ShouldUpdateEmail()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var newEmail = faker.Internet.Email();

        user.UpdateEmail(newEmail);

        Assert.Equal(newEmail, user.Email.Value);
    }

    [Theory]
    [InlineData("short")]
    [InlineData("12345678")]
    [InlineData("NoDigits!")]
    [InlineData("NoSpecial123")]
    [InlineData("")]
    public async Task UpdatePassword_ShouldThrowException_WhenInvalid(string invalidPassword)
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        Assert.Throws<ArgumentException>(() => user.UpdatePassword(invalidPassword));
    }

    [Fact]
    public async Task UpdatePassword_ShouldUpdatePassword()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var newPassword = "NewValid@456";

        user.UpdatePassword(newPassword);

        Assert.Equal(newPassword, user.Password.Value);
    }
}
