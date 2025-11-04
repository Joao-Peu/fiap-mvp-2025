using Bogus;
using FIAPCloudGames.Application.Services;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Exceptions;
using FIAPCloudGames.Domain.Interfaces;
using NSubstitute;

namespace FIAPCloudGames.Tests.Application;

public class UserServiceTests
{
    private readonly IUserRepository _userRepository;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_userRepository);
    }

    [Fact]
    public async Task Register_ShouldReturnUser_WhenValidData()
    {
        var faker = new Faker();
        var name = faker.Person.FullName;
        var email = faker.Internet.Email();
        var password = "Valid@123";

        var userDto = await _userService.RegisterAsync(name, email, password);

        Assert.NotNull(userDto);
        Assert.Equal(name, userDto.Name);
        Assert.Equal(email, userDto.Email);
        await _userRepository.Received(1).AddAsync(Arg.Any<User>());
    }

    [Fact]
    public async Task Register_ShouldThrowException_WhenEmailExists()
    {
        var faker = new Faker();
        var name = faker.Person.FullName;
        var email = faker.Internet.Email();
        var password = "Valid@123";

        _userRepository.EmailExistsAsync(email).Returns(true);

        await Assert.ThrowsAsync<EmailAlreadyExistsForUserException>(async () =>
            await _userService.RegisterAsync(name, email, password));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public async Task Register_ShouldThrowException_WhenNameIsNullOrWhiteSpace(string name)
    {
        var faker = new Faker();
        var email = faker.Internet.Email();
        var password = "Valid@123";

        await Assert.ThrowsAnyAsync<ArgumentException>(async () =>
            await _userService.RegisterAsync(name, email, password));
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("@example.com")]
    [InlineData("test@")]
    public async Task Register_ShouldThrowException_WhenEmailIsInvalid(string invalidEmail)
    {
        var faker = new Faker();
        var name = faker.Person.FullName;
        var password = "Valid@123";

        await Assert.ThrowsAsync<EmailInvalidException>(async () =>
            await _userService.RegisterAsync(name, invalidEmail, password));
    }

    [Theory]
    [InlineData("short")]
    [InlineData("12345678")]
    [InlineData("NoDigits!")]
    [InlineData("NoSpecial123")]
    public async Task Register_ShouldThrowException_WhenPasswordIsInvalid(string invalidPassword)
    {
        var faker = new Faker();
        var name = faker.Person.FullName;
        var email = faker.Internet.Email();

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _userService.RegisterAsync(name, email, invalidPassword));
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllUsers()
    {
        var users = new List<User>
        {
            new User("User One", "user1@example.com", "Valid@123"),
            new User("User Two", "user2@example.com", "Valid@456"),
            new User("User Three", "user3@example.com", "Valid@789")
        };

        _userRepository.GetAllAsync().Returns(users);

        var result = await _userService.GetAllAsync();

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAll_ShouldReturnEmptyList_WhenNoUsers()
    {
        _userRepository.GetAllAsync().Returns(new List<User>());

        var result = await _userService.GetAllAsync();

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenUserNotFound()
    {
        var nonExistentId = Guid.NewGuid();
        _userRepository.GetByIdAsync(nonExistentId).Returns((User?)null);

        var result = await _userService.GetByIdAsync(nonExistentId);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetById_ShouldReturnUser_WhenUserExists()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        _userRepository.GetByIdAsync(user.Id).Returns(user);

        var result = await _userService.GetByIdAsync(user.Id);

        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Name, result.Name);
        Assert.Equal(user.Email.Value, result.Email);
    }

    [Fact]
    public async Task Update_ShouldReturnNull_WhenUserNotFound()
    {
        var nonExistentId = Guid.NewGuid();
        var faker = new Faker();

        _userRepository.GetByIdAsync(nonExistentId).Returns((User?)null);

        var result = await _userService.UpdateAsync(
            nonExistentId,
            faker.Person.FullName,
            faker.Internet.Email(),
            "Valid@123"
        );

        Assert.Null(result);
        await _userRepository.DidNotReceive().UpdateAsync(Arg.Any<User>());
    }

    [Fact]
    public async Task Update_ShouldUpdateUser_WhenUserExists()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");
        var newName = faker.Person.FullName;
        var newEmail = faker.Internet.Email();
        var newPassword = "NewValid@456";

        _userRepository.GetByIdAsync(user.Id).Returns(user);

        var result = await _userService.UpdateAsync(user.Id, newName, newEmail, newPassword);

        Assert.NotNull(result);
        Assert.Equal(newName, result.Name);
        Assert.Equal(newEmail, result.Email);
        await _userRepository.Received(1).UpdateAsync(user);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public async Task Update_ShouldThrowException_WhenNameIsInvalid(string invalidName)
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        _userRepository.GetByIdAsync(user.Id).Returns(user);

        await Assert.ThrowsAnyAsync<ArgumentException>(async () =>
            await _userService.UpdateAsync(user.Id, invalidName, faker.Internet.Email(), "Valid@123"));
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("@example.com")]
    [InlineData("test@")]
    public async Task Update_ShouldThrowException_WhenEmailIsInvalid(string invalidEmail)
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        _userRepository.GetByIdAsync(user.Id).Returns(user);

        await Assert.ThrowsAsync<EmailInvalidException>(async () =>
            await _userService.UpdateAsync(user.Id, faker.Person.FullName, invalidEmail, "Valid@123"));
    }

    [Theory]
    [InlineData("short")]
    [InlineData("12345678")]
    [InlineData("NoDigits!")]
    [InlineData("NoSpecial123")]
    public async Task Update_ShouldThrowException_WhenPasswordIsInvalid(string invalidPassword)
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        _userRepository.GetByIdAsync(user.Id).Returns(user);

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _userService.UpdateAsync(user.Id, faker.Person.FullName, faker.Internet.Email(), invalidPassword));
    }

    [Fact]
    public async Task Delete_ShouldReturnFalse_WhenUserNotFound()
    {
        var nonExistentId = Guid.NewGuid();
        _userRepository.GetByIdAsync(nonExistentId).Returns((User?)null);

        var result = await _userService.DeleteAsync(nonExistentId);

        Assert.False(result);
        await _userRepository.DidNotReceive().RemoveAsync(Arg.Any<User>());
    }

    [Fact]
    public async Task Delete_ShouldDeleteUser_WhenUserExists()
    {
        var faker = new Faker();
        var user = new User(faker.Person.FullName, faker.Internet.Email(), "Valid@123");

        _userRepository.GetByIdAsync(user.Id).Returns(user);

        var result = await _userService.DeleteAsync(user.Id);

        Assert.True(result);
        await _userRepository.Received(1).RemoveAsync(user);
    }

    [Fact]
    public async Task Authenticate_ShouldReturnNull_WhenUserNotFound()
    {
        var email = "nonexistent@example.com";
        var password = "Valid@123";

        _userRepository.GetByEmailAsync(email).Returns((User?)null);

        var result = await _userService.AuthenticateAsync(email, password);

        Assert.Null(result);
    }

    [Fact]
    public async Task Authenticate_ShouldReturnNull_WhenPasswordIsIncorrect()
    {
        var faker = new Faker();
        var correctPassword = "Valid@123";
        var user = new User(faker.Person.FullName, faker.Internet.Email(), correctPassword);

        _userRepository.GetByEmailAsync(user.Email.Value).Returns(user);

        var result = await _userService.AuthenticateAsync(user.Email.Value, "WrongPassword@456");

        Assert.Null(result);
    }

    [Fact]
    public async Task Authenticate_ShouldReturnUser_WhenCredentialsAreValid()
    {
        var faker = new Faker();
        var password = "Valid@123";
        var user = new User(faker.Person.FullName, faker.Internet.Email(), password);

        _userRepository.GetByEmailAsync(user.Email.Value).Returns(user);

        var result = await _userService.AuthenticateAsync(user.Email.Value, password);

        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Email.Value, result.Email.Value);
        Assert.Equal(user.Name, result.Name);
    }
}
